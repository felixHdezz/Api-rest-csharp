using easyBotQaNApi.api.DataServices;
using easyBotQaNApi.api.Infrastructure.ContentNegotiation;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Infrastructure;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using easyBotQaNApi.api.Security;

namespace easyBotQaNApi.api
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

			var config = new HttpConfiguration();

			//Setup response settings
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = ContentNegotiationUtils.contractResolver;
			config.MapHttpAttributeRoutes();

			//Setup Dependency Resolver
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
			foreach (var implementation in ServiceResolver.GetImplementations())
			{
				container.Register(implementation.Key, implementation.Value, SimpleInjector.Lifestyle.Singleton);
			}

			container.Register<IEmailService, EmailServices>();

			container.RegisterWebApiControllers(config);

			container.Verify();
			config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.MessageHandlers.Add(new TokenValidationHandler());
            //// WEB API SERVICES


            // Setup Jwt Authentication
            app.UseCors(CorsOptions.AllowAll);

			config.RegisterSwagger();

			// Setup auto api documentation and auto UI
			// Webapi configuration
			app.UseWebApi(config);
		}
	}
}

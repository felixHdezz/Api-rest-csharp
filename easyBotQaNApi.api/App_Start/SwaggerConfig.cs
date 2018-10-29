using System.Web.Http;
using WebActivatorEx;
using easyBotQaNApi.api;
using Swashbuckle.Application;
using System;
using System.Web;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace easyBotQaNApi.api
{
    public static class SwaggerConfig
    {
        public static void RegisterSwagger(this HttpConfiguration Configuration)
        {
            //var thisAssembly = typeof(SwaggerConfig).Assembly;

            Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "easyBotQaNApi.api");
						c.RootUrl(req => new Uri(req.RequestUri, HttpContext.Current.Request.ApplicationPath ?? string.Empty).ToString());
						
					})
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}

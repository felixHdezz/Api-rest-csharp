using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace easyBotQaNApi.api.Infrastructure.Controllers
{
    public abstract class BaseController : ApiController
    {
		protected string GetBody(string templatePath)
		{
			using (StreamReader reader = new StreamReader(Map(templatePath)))
			{
				return reader.ReadToEnd();
			}
		}

		protected string Map(string relativePath)
		{
			return HttpContext.Current.Request.MapPath(relativePath);
		}
	}
}

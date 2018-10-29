using System.Web.Http;

namespace easyBotQaNApi.api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		#region Protected Methods

		private const string ServerName = "EasyBot";

		protected void Application_PreSendRequestHeaders()
		{
			Response.Headers.Set("Server", ServerName);
		}

		#endregion Protected Methods
	}
}
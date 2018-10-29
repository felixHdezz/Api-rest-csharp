using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace easyBotQaNApi.api.Infrastructure.Controllers
{
    public abstract class EmailSenderController : BaseController
    {
		#region Public Constructors

		protected EmailSenderController(IEmailService emailService)
		{
			EmailService = emailService;
		}

		#endregion Public Constructors

		#region Protected Properties

		protected IEmailService EmailService { get; }

		#endregion Protected Properties

		#region Protected Methods

		protected IdentityMessage GetIdentityMessage(string destination, string subject, string templatePath, Dictionary<string, string> variables)
		{
			var body = GetBodyFromTemplate(templatePath, variables);
			return new IdentityMessage
			{
				Destination = destination,
				Subject = subject,
				Body = body
			};
		}

		protected LinkedResource GetResource(string path)
		{
			return new LinkedResource(Map(path)) { ContentId = Guid.NewGuid().ToString() };
		}

		protected async Task<ContactAreaModel> getDataContact(int IdArea) {
			return await EmailService.getDataContact(IdArea);
		}

		protected async Task<ContactAreaModel> getDataUser(string username) {
			return await EmailService.getDataUser(username);
		}

		#endregion Protected Methods

		#region Private Methods

		private string GetBodyFromTemplate(string templatePath, Dictionary<string, string> variables)
		{
			var body = GetBody(templatePath);
			foreach (var pair in variables)
			{
				body = body.Replace(pair.Key, pair.Value);
			}
			return body;
		}

		#endregion Private Methods
	}

}

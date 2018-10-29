using easyBotQaNApi.api.Infrastructure;
using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Configuration;

using System.Threading.Tasks;
using easyBotQaNApi.api.DataServices.IServices;

namespace easyBotQaNApi.api.Controllers
{
	[RoutePrefix("api/contact")]
    public class EmailController : EmailSenderController
    {
		public EmailController(IEmailService emailService) : base(emailService)
		{
			//Initialize constructor
		}

		[AllowAnonymous]
		[Route("sendMessage")]
		[HttpPost]
		public async Task<IHttpActionResult> sendMessege(MensajeModel model) {
			//var mailNotifier = ConfigurationManager.AppSettings["mailContact"];
			var dataContact = await getDataContact(model.idArea);

			var variables = new Dictionary<string, string>();

			variables.Add("{{ContactName}}", dataContact.ContactName);
			variables.Add("{{name}}", model.Name);
			variables.Add("{{email}}", model.Email);
			variables.Add("{{message}}", model.Question);
			variables.Add("{{ConfirmationHref}}", model.Url);

			var identityMessage = GetIdentityMessage(dataContact.Email, "Contacto", ResourcePaths.SEND_MESSAGE_TEMPLATE, variables);
			await EmailService.SendAsync(identityMessage);

			return Ok();
		}

		[AllowAnonymous]
		[Route("sendMessegeToUser")]
		[HttpPost]
		public async Task<IHttpActionResult> sendMessegeToUser(MensajeModel model) {
			var dataContact = await getDataContact(model.idArea);

			var variables = new Dictionary<string, string>();

			variables.Add("{{name}}", dataContact.ContactName);
			variables.Add("{{question}}", model.Question);
			variables.Add("{{answer}}", model.Answer);

			var identityMessage = GetIdentityMessage(dataContact.Email, "Contacto", ResourcePaths.SEND_MESSAGE_USER_TEMPLATE, variables);
			await EmailService.SendAsync(identityMessage);

			return Ok();
		}

	}
}

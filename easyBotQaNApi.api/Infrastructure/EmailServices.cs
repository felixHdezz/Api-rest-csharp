using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace easyBotQaNApi.api.Infrastructure
{
	public class EmailServices : IEmailService
	{
		#region Private Properties

		private string MailAddress => ConfigurationManager.AppSettings["mailNotifier"];

		#endregion Private Properties

		#region Public Methods

		public async Task SendAsync(IdentityMessage message)
		{
			await SendAsync(message, new List<LinkedResource>(), new List<Attachment>());
		}

		public async Task SendAsync(IdentityMessage identityMessage, List<LinkedResource> linkedResources)
		{
			await SendAsync(identityMessage, linkedResources, new List<Attachment>());
		}

		public async Task SendAsync(IdentityMessage identityMessage, List<Attachment> attachments)
		{
			await SendAsync(identityMessage, new List<LinkedResource>(), attachments);
		}

		public async Task SendAsync(IdentityMessage identityMessage, List<LinkedResource> linkedResources, List<Attachment> attachments)
		{
			var client = new SmtpClient();
			var mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(MailAddress);
			mailMessage.To.Add(identityMessage.Destination);
			mailMessage.Subject = identityMessage.Subject;
			mailMessage.Body = identityMessage.Body;
			mailMessage.IsBodyHtml = true;
			await client.SendMailAsync(mailMessage);
		}

		public async Task<ContactAreaModel> getDataContact(int IdArea)
		{
			ContactAreaModel model = new ContactAreaModel();
			using (var _context = new DataBaseContext())
			{
				object[] parameter = new object[] { IdArea };
				var dReader = await _context.ExecuteReader("sp_GetAreaContact", parameter);
				while (dReader.Read())
				{
					model.ContactName = dReader.GetString(0);
					model.Email = dReader.GetString(1);
				}
			}
			return model;
		}

		public async Task<ContactAreaModel> getDataUser(string username)
		{
			ContactAreaModel model = new ContactAreaModel();
			using (var _context = new DataBaseContext())
			{
				var strQuery = "EXEC sp_GetDataUser '" + username + "'";
				object[] parameter = new object[] { username };
				var dReader = await _context.ExecuteReader("sp_GetDataUser", parameter);
				while (dReader.Read())
				{
					model.ContactName = dReader.GetString(0);
					model.Email = dReader.GetString(1);
				}
			}
			return model;
		}
		#endregion Public Methods
	}
}
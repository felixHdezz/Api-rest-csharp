using easyBotQaNApi.api.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.IServices
{
	public interface IEmailService : IIdentityMessageService
	{
		#region Public Methods

		Task SendAsync(IdentityMessage identityMessage, List<LinkedResource> linkedResources);

		Task SendAsync(IdentityMessage identityMessage, List<Attachment> attachments);

		Task SendAsync(IdentityMessage identityMessage, List<LinkedResource> linkedResources, List<Attachment> attachments);

		Task<ContactAreaModel> getDataContact(int IdArea);

		Task<ContactAreaModel> getDataUser(string username);

		#endregion Public Methods
	}
}
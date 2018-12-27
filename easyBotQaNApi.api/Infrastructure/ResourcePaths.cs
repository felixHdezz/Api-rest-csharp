using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Infrastructure
{
	public static class ResourcePaths
	{
		public const string CONFIRM_ACCOUNT_TEMPLATE = "~/Content/EmailTemplate/confirm_account_template.html";
		public const string LOGO = "~/Content/Images/logo.png";
		public const string SEND_MESSAGE_TEMPLATE = "~/Content/EmailTemplate/send_message_template.html";
		public const string SEND_MESSAGE_USER_TEMPLATE = "~/Content/EmailTemplate/send_message_user_template.html";
        public const string EXPORT_QUESTIONS_TEMPLATE = "~/Content/Excel/export_questions_template.xlsx";

        ///public const string EXPORT_QUESTIONS_TEMPLATE = HttpContext.Current.Server.MapPath("~/Content/Excel/export_questions_template.xlsx");
    }
}
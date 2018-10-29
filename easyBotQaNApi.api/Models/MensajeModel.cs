using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class MensajeModel
	{
		public string Name { get; set; }
		public string userName { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public string Email { get; set; }
		public int idArea { get; set; }
		public string Url { get; set; }
	}
}
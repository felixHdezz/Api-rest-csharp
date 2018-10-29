using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class registerUserAreaModel
	{
		public int idArea { get; set; }
		public string name { get; set; }
		public string userName { get; set; }
		public string email { get; set; }
	}
}
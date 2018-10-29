using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class SaveAnswerModel
	{
		public int idArea { get; set; }
		public string question { get; set; }
		public string answer { get; set; }
		public string user { get; set; }
	}
}
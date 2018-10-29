using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class EndPointModel
	{
		public int id { get; set; }
		public string keyId { get; set; }
		public string endpointKey { get; set; }
		public string hostname { get; set; }
	}
}
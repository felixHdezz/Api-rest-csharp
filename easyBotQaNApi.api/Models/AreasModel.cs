using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class AreasModel
	{
		public int id { get; set; }
		public string area { get; set; }
	}

    public class AreasKeyIdModel {
        public string KeId { get; set; }
    }

    public class GetAllQuestionByArea {
        public string Question { get; set; }
        public string Regions { get; set; }
        public string Answer { get; set; }
    }

    public class JsonQuestion {
        public string strQuestion { get; set; }
    }

    public class validAreas {
        public string email { get; set; }
    }
}
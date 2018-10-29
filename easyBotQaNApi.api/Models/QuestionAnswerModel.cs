﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class QuestionAnswerModel
	{
		public int id { get; set; }
		public string answer { get; set; }
		public string source { get; set; }
		public string[] questions { get; set; }
		public string[] metadata { get; set; }
	}

	public class questions {
		public string question { get; set; }
	}
}
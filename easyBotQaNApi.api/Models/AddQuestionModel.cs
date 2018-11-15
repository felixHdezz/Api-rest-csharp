using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class AddQuestionModel
    {
        public int IdArea { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        public int IsActive { get; set; }
    }
}
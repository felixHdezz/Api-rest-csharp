using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class RegionsModel
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public int IsActive { get; set; }
    }

    public class UpdateRegionModel {
        public int Id { get; set; }
        public int IsActive { get; set; }
    }
}
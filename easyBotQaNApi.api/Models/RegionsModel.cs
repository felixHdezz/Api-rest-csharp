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

    public class CrudRegionsModel
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public int IsActive { get; set; }
        public string IdAreas { get; set; }
    }
    public class SaveNewQuestion
    {
        public int Question { get; set; }
        public string Answer { get; set; }
        public int IdArea { get; set; }
        public string IdRegions { get; set; }
        public string Type { get; set; }
    }

    public class GetMessage
    {
        public int IdQuestion { get; set; }
        public string Region { get; set; }
        public string IdArea { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class OrganizationUnitModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
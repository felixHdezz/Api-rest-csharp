using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class EnvironmentsModel
    {
        /// <summary>
        /// Modelo en ambientes
        /// </summary>
        public int Id { get; set; }
        public string Environment { get; set; }
        public string HostName { get; set; }
        public string EndPointKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
    }
}
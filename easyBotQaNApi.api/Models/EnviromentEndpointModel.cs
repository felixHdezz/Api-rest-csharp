using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class EndPointsModel
    {
        public List<EnviromentEndpointModel> endpoints { get; set; }
    }
    public class EnviromentEndpointModel
    {
        public string environment { get; set; }
        public string qnaAuthKey { get; set; }
        public string qnaKBId { get; set; }
        public string endpointHostName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
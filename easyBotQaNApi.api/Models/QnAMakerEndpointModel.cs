using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class QnAMakerEndpointModel
    {
        public string qnaAuthKey { get; set; }
        public string qnaKBId { get; set; }
        public string endpointHostName { get; set; }
    }
}
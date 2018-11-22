using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
    public class KnowledgeBasesModel
    {
        public int Id { get; set; }
        public string KnowledgeBase { get; set; }
        public string Description { get; set; }
        public string KeyId { get; set; }
        public int Id_contact { get; set; }
        public string Contact { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id_env { get; set; }
        public string Environment { get; set; }
    }
}
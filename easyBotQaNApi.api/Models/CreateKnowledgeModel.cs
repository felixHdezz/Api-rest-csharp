using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyBotQaNApi.api.Models
{
	public class CreateKnowledgeModel
	{
		public string NombreResponsable { get; set; }
		public string Apellidos { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string NombreConocimiento { get; set; }
	}
}
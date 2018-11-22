using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;

namespace easyBotQaNApi.api.DataServices
{
	public class ServiceResolver
	{
		public static Dictionary<Type, Type> GetImplementations()
		{
			var dictionary = new Dictionary<Type, Type>();
			//Se agrego nueva referencia para el log de registros
			dictionary.Add(typeof(IAreasServices), typeof(AreasServices));
			dictionary.Add(typeof(IKnowledgeBaseServices), typeof(KnowledgeBaseServices));
            dictionary.Add(typeof(IRegionsService), typeof(RegionsService));
            return dictionary;
		}
	}
}
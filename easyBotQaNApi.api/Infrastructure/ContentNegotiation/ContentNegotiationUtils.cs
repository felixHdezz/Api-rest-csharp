using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace easyBotQaNApi.api.Infrastructure.ContentNegotiation
{
	public static class ContentNegotiationUtils
	{
		#region Public Fields

		public static readonly JsonSerializerSettings serializationSettings;
		public static readonly IContractResolver contractResolver = new CamelCasePropertyNamesContractResolver();
		public static readonly string defaultMediaType = "application/json";
		public static readonly Encoding encoding = Encoding.UTF8;

		#endregion Public Fields

		static ContentNegotiationUtils()
		{
			serializationSettings = new JsonSerializerSettings { ContractResolver = contractResolver };
		}
	}
}
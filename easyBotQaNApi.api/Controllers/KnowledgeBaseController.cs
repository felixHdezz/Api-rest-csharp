using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using ExcelDataReader;

namespace easyBotQaNApi.api.Controllers
{
	[RoutePrefix("api/knowledgebase")]
	public class KnowledgeBaseController : ApiController
	{
		#region constructor

		public KnowledgeBaseController(IKnowledgeBaseServices knowledgeBaseServices) {
			services = knowledgeBaseServices;
		}
		#endregion constructor

		public IKnowledgeBaseServices services { get; set; }

		#region methods public

		[Route("{strArea}")]
		[HttpGet]
		public async Task<IHttpActionResult> Get(string strArea)
		{
			var result = await services.GetQuestionAnswersByIdArea(strArea);
			return Ok(result);
		}

		[Route("createKnowledge")]
		[HttpPost]
		public async Task<IHttpActionResult> createKnowledge()
		{
			byte[] _file = null;
			var result = 0;
			CreateKnowledgeModel model = new CreateKnowledgeModel
			{
				NombreResponsable = Convert.ToString(HttpContext.Current.Request["NombreResponsable"]),
				Apellidos = Convert.ToString(HttpContext.Current.Request["Apellidos"]),
				Telefono = Convert.ToString(HttpContext.Current.Request["Telefono"]),
				Email = Convert.ToString(HttpContext.Current.Request["Email"]),
				NombreConocimiento = Convert.ToString(HttpContext.Current.Request["NombreConocimiento"])
			};

			//Obtiene el archivo excel
			var files = HttpContext.Current.Request.Files;
			if (files.Count > 0)
			{
				for (int i = 0; i < files.Count; i++)
				{
					var file = files[i];

					DataTable _table = null;

					BinaryReader reader = new BinaryReader(files[i].InputStream);  //FileUpload1.PostedFile.InputStream)
					_file = reader.ReadBytes(files[i].ContentLength);

					_table = ConvertToDataTale(files[i].InputStream);

					//DataTable _DataReader = ByteBufferToTable(_file, true);

					result = await services.saveAnswerKnowledge(_table, model);
				}
			}
			else
			{
				return BadRequest("Nodata");
			}
			return Ok();
		}

		[Route("updateAreaKeyId")]
		[HttpPost]
		public async Task<IHttpActionResult> updateAreaKeyId(AreaEndPointModel model) {
			var result = await services.updateAreaKeyId(model);
			return Ok(result);
		}

		[Route("saveAnswer")]
		[HttpPost]
		public async Task<IHttpActionResult> saveAnswer(SaveAnswerModel model)
		{
			var result = await services.saveAnswer(model);
			return Ok(result);
		}

        [Route("SaveQuestion")]
        [HttpPost]
        public async Task<IHttpActionResult> saveQuestion(AddQuestionModel addQuestionModel) {

            return Ok();
        }

        #endregion methods public

        #region methods private 

        private static DataTable ByteBufferToTable(byte[] buffer, bool includeHeader)
		{
			// Se asume que el separador de decimales es punto "." y el de miles "," (aunque este ultimo no se usa) 
			CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Dictionary<string, int> indexs = new Dictionary<string, int>();
			DataTable dt = new DataTable();
			char[] delimiter = new char[] { ',' };

			using (StreamReader sr = new StreamReader(new MemoryStream(buffer)))
			{
				try
				{
					int rowsCompleted = 0;
					int lastLength = 0;
					bool readHeader = true;

					while (sr.Peek() > -1)
					{
						bool addLine = true;
						string line = sr.ReadLine();
						string[] lineArray = line.Split(delimiter);

						//Se chequea que tanto el orden como el nombre de las columnas correspondan según el orden dado
						if (readHeader)
						{
							if (includeHeader)
							{
								int j = 0;
								foreach (string column in lineArray)
								{
									DataColumn c = new DataColumn(column);
									dt.Columns.Add(c);
									indexs.Add(column, j);
									j++;
								}
								//Se continua con la lectura del archivo
								line = sr.ReadLine();
								lineArray = line.Split(delimiter);
							}
							//Se cambia el estado de esta variable para no volver a chequear el header
							readHeader = false;
						}

						DataRow nuevaFila = dt.NewRow();
						if (lastLength > 0 && lastLength != lineArray.Length)
						{
							continue;
						}
						lastLength = lineArray.Length;
						try
						{
							foreach (DataColumn column in dt.Columns)
							{
								int index = indexs[column.ColumnName];
								string colName = column.ColumnName;
								string value = lineArray[index];
								nuevaFila[colName] = string.IsNullOrEmpty(value) ? DBNull.Value + "" : value;
							}
						}
						catch (Exception e)
						{
							throw e;
						}
						if (addLine)
						{
							dt.Rows.Add(nuevaFila);
							rowsCompleted++;
						}
					}
					return dt;
				}
				finally
				{
					System.Threading.Thread.CurrentThread.CurrentCulture = culture;
				}
			}
		}

		private static DataTable ConvertToDataTale(Stream stream) {
			DataTable _table = null;
			DataTable _newTable = new DataTable();
			string[] header = new string[] { "Pregunta", "Respuesta" };

			using (var _reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
			{
				var _result = _reader.AsDataSet();
				// Ejemplos de acceso a datos
				_table = _result.Tables[0];

				bool readHeader = true;

				for (var _i = 0; _i < _table.Rows.Count; _i++) {
					if (readHeader)
					{
						foreach (string column in header)
						{
							DataColumn c = new DataColumn(column);
							_newTable.Columns.Add(c);
						}
						readHeader = false;
					}
					if (!readHeader && _i != 0)
					{
						DataRow _newRow = _newTable.NewRow();
						int _cont = 0;
						foreach (DataColumn column in _newTable.Columns)
						{
							_newRow[column.ColumnName] = _table.Rows[_i][_cont];
							_cont++;
						}
						//Agrega el nuevo row
						_newTable.Rows.Add(_newRow);
					}
				}
			}
			return _newTable;
		}

		#endregion methods private 
	}
}

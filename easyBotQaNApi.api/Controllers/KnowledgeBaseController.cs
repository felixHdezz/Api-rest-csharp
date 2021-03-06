﻿using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using easyBotQaNApi.api.Infrastructure;
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
        [Authorize]
        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get() {
            var result = await services.GetKnowledgesBases();
            return Ok(result);
        }

        [Route("{strArea}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string strArea)
        {
            var result = await services.GetQuestionAnswersByIdArea(strArea);
            return Ok(result);
        }

        [Route]
        [HttpPut]
        public async Task<IHttpActionResult> Put(KnowledgeBasesModel model) {
            var result = await services.UpdateKnowledgeBase(model);
            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id) {
            var result = await services.DeleteKnowledgeBase(id);
            return Ok(result);
        }

        [Authorize]
        [Route("createKnowledge")]
        [HttpPost]
        public async Task<IHttpActionResult> createKnowledge()
        {
            byte[] _file = null;
            var result = new List<QuestionAnswerModel>();
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

                    try
                    {
                        result = await services.SaveAnswerKnowledge(_table, model);
                    }
                    catch (Exception ex) {
                        return Ok(ex.Message.ToString());
                    }
                }
            }
            else
            {
                return BadRequest("Nodata");
            }
            return Ok(result);

        }

        [Authorize]
        [Route("updateAreaKeyId")]
        [HttpPost]
        public async Task<IHttpActionResult> updateAreaKeyId(AreaEndPointModel model) {
            var result = await services.UpdateAreaKeyId(model);
            return Ok(result);
        }

        [Route("saveAnswer")]
        [HttpPost]
        public async Task<IHttpActionResult> saveAnswer(SaveAnswerModel model)
        {
            var result = await services.SaveAnswer(model);
            return Ok(result);
        }

        [Route("SaveQuestion")]
        [HttpPost]
        public async Task<IHttpActionResult> saveQuestion(AddQuestionModel addQuestionModel) {
            var result = await services.AddQuestion(addQuestionModel);
            return Ok(result);
        }

        [Authorize]
        [Route("getEnvironments")]
        [HttpGet]
        public async Task<IHttpActionResult> getEnvironments() {
            try
            {
                var result = await services.GetEnvironments();
                return Ok(result);
            }
            catch (Exception ex) {
                return Ok(ex.Message.ToString());
            }
        }

        [Authorize]
        [Route("crud_environment")]
        [HttpPost]
        public async Task<IHttpActionResult> crud_environment(EnvironmentsModel model) {
            var result = await services.Crud_Environment(model);
            return Ok(result);
        }

        [Route("UpdateStatusAEnv")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateStatusAEnv(int Id, int Status)
        {
            var result = await services.UpdateStatusEnv(Id, Status);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("GetEndPointQaNMaker/{username}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEndPointQaNMaker(string username)
        {
            var result = await services.GetEndPointQaNMaker(username);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("getMessage")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAnswerForUser(GetMessage model)
        {
            var result = await services.GetAnswerForUser(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("replacetext/{text}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetReplaceText(string text) {
            var result = await services.GetReplaceText(text);
            return Ok(result);
        }

        [Authorize]
        [Route("updateknowledge")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateKnowledge() {
            byte[] _file = null;
            var result = new List<QuestionAnswerModel>();

            var nameKnowledge = Convert.ToString(HttpContext.Current.Request["NombreConocimiento"]);

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
                    result = await services.UpdateKnowledge(nameKnowledge, _table);
                }
            }
            else
            {
                return BadRequest("Nodata");
            }
            return Ok(result);
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
            string[] header = new string[] { "Id", "Pregunta", "Region", "Respuesta" };

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
                            if (column.ColumnName == "Id")
                            {
                                _newRow[column.ColumnName] = _i;
                            }
                            else {
                                _newRow[column.ColumnName] = _table.Rows[_i][_cont];
                            }
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

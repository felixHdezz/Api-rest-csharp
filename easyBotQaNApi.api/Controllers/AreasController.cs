using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Web;
using easyBotQaNApi.api.Infrastructure;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections;
using System.Web.Cors;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace easyBotQaNApi.api.Controllers
{ 
    [RoutePrefix("api/areas")]
    public class AreasController : ApiController
    {
        protected string _template_path = HttpContext.Current.Server.MapPath("~/Content/Excel/export_all_questions_template.xlsx");

        protected string _template_path_copy = HttpContext.Current.Server.MapPath("~/Content/Excel/Files");

        #region constructor

        public AreasController(IAreasServices _IAreasServices)
        {
            _Services = _IAreasServices;
        }

        #endregion constructor

        #region methods public

        private IAreasServices _Services { get; }

        [AllowAnonymous]
        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _Services.getEndPoint();
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _Services.getAreaById(id);
            return Ok(result);
        }

        [Authorize]
        [Route("updateUserArea/{idArea}")]
        [HttpPost]
        public async Task<IHttpActionResult> updateUserArea(int idArea)
        {
            var userName = HttpContext.Current.User.Identity.Name.ToString();
            var result = await _Services.updateAreaByuser(idArea);

            return Ok(result);
        }

        [AllowAnonymous]
        [Route("registerUserArea")]
        [HttpPost]
        public async Task<IHttpActionResult> registerUserArea(registerUserAreaModel userArea)
        {
            if (userArea.idArea != 0 && userArea.userName != null && userArea.email != null)
            {
                return Ok(await _Services.registerUserArea(userArea));
            }
            else
            {
                return BadRequest("data invalid");
            }
        }

        [AllowAnonymous]
        [Route("GetKeyId/{IdAreas}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetKeyId(string IdAreas)
        {
            var _result = await _Services.GetKeyId(IdAreas);
            return Ok(_result);
        }

        [AllowAnonymous]
        [Route("GetIdArea/{username}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetIdArea(string username)
        {
            var _result = await _Services.GetIdArea(username);
            return Ok(_result);
        }

        [Authorize]
        [Route("getAllQuestion/{idArea}")]
        [HttpPost]
        public async Task<IHttpActionResult> getAllQuestion(int idArea, JsonQuestion model)
        {
            try
            {
                var _listQuestionQnAMaker = JsonConvert.DeserializeObject<List<QuestionQnAMaker>>(model.strQuestion);

                var _dataTable = ConvertToDataTable(_listQuestionQnAMaker);

                var _result = new List<GetAllQuestionByArea>();

                _result = await _Services.GetAllQuestionByArea(idArea, _dataTable);

                CreateFileExcel(_result);

                byte[] _data_array = convertToByteFile(_template_path);

                var _base64String = Convert.ToBase64String(_data_array);

                return Ok(_base64String);
            }
            catch (Exception ex) {
                return Ok(ex.Message.ToString());
            }
        }


        [AllowAnonymous]
        [Route("validGetArea")]
        [HttpPost]
        public async Task<IHttpActionResult> ValidGetArea(validAreas model) {
            var result = await _Services.ValidGetArea(model.email);
            return Ok(result);
        }

        #endregion methods public

        #region methods private

        private byte[] convertToByteFile(string _path)
        {
            FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();

            return ImageData; //return the byte data
        }

        private void CreateFileExcel(List<GetAllQuestionByArea> _data)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(_template_path, true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                //Elimina todos los datos del archivo
                sheetData.RemoveAllChildren();

                //Crea el header el archivo
                Row _header = new Row();
                _header.Append(
                        ConstructCell("Id", CellValues.String),
                        ConstructCell("Pregunta", CellValues.String),
                        ConstructCell("Región", CellValues.String),
                        ConstructCell("Respuesta", CellValues.String));
                //Guarda los cambios del header
                sheetData.AppendChild(_header);

                //Agrega lo registros, de cada fila
                Row _row;
                for (var _i = 0; _i < _data.Count; _i++)
                {
                    _row = new Row();

                    _row.Append(
                        ConstructCell((_i + 1).ToString(), CellValues.String),
                        ConstructCell(_data[_i].Question.ToString(), CellValues.String),
                        ConstructCell(_data[_i].Regions.ToString() != "" ? _data[_i].Regions.ToString() : "Global", CellValues.String),
                        ConstructCell(_data[_i].Answer.ToString(), CellValues.String));

                    sheetData.AppendChild(_row);
                }
                worksheetPart.Worksheet.Save();
            }
        }

        private Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }

        private DataTable ConvertToDataTable(List<QuestionQnAMaker> _data) {
            var _rTable = new DataTable();
            bool readHeader = true;
            //verifinir cabecera  de la tabla
            string[] header = new string[] { "question", "answer" };
            for (var _index = 0; _index < _data.Count; _index++) {
                if (readHeader) {
                    foreach (string column in header)
                    {
                        DataColumn c = new DataColumn(column);
                        _rTable.Columns.Add(c);
                    }
                    readHeader = false;
                }
                if (!readHeader)
                {
                    DataRow _newRow = _rTable.NewRow();
                    int _cont = 0;
                    string[] _rowdata = new string[] { _data[_index].Question, _data[_index].Answer };
                    foreach (DataColumn column in _rTable.Columns)
                    {
                        _newRow[column.ColumnName] = _rowdata[_cont];
                        _cont++;
                    }

                    //Agrega el nuevo row
                    _rTable.Rows.Add(_newRow);
                }
            }

            return _rTable;
        }

        #endregion methods pivate
    }
}

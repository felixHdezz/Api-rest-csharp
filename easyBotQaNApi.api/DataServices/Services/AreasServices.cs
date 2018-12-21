using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyBotQaNApi.api.Models;
using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using System.Threading.Tasks;
using System.Data;

namespace easyBotQaNApi.api.DataServices.Services
{
	public class AreasServices : IAreasServices
	{
        #region public methods

        public async Task<List<AreasModel>> getEndPoint()
		{
			var listEndPoint = new List<AreasModel>();
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { };
				var dReader = await _context.ExecuteReaderAsync("SP_GETAreas", parameters);
				while (dReader.Read())
				{
					var area = new AreasModel();
					area.id = dReader.GetInt32(0);
					area.area = dReader.GetString(1);

					listEndPoint.Add(area);
				}
			}
			return listEndPoint;
		}

		public async Task<AreaEndPointModel> getAreaById(int id)
		{
			AreaEndPointModel areaEndPointModel = new AreaEndPointModel();
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { id };
				var dReader = await _context.ExecuteReaderAsync("sp_GetKeyIdArea", parameters);
				while (dReader.Read())
				{
					areaEndPointModel.Area = dReader.GetString(0);
					areaEndPointModel.KeyId = dReader.GetString(1);
				}
			}
			return areaEndPointModel;
		}

		public async Task<int> updateAreaByuser(int idArea)
		{
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { idArea };
				return await _context.ExecuteNonQueryAsync("SP_UpdateEndPointTest", parameters);
			}
		}

		public async Task<int> registerUserArea(registerUserAreaModel userArea)
		{
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { userArea.name, userArea.userName, userArea.email, userArea.idArea };
				return await _context.ExecuteNonQueryAsync("sp_updateUserArea", parameters);
			}
		}

        public async Task<List<AreasKeyIdModel>> GetKeyId(string IdAreas)
        {
            var _keyIds = new List<AreasKeyIdModel>();
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { IdAreas };
                var _reader = await _context.ExecuteReaderAsync("sp_GetKeyId", parameters);
                while (_reader.Read())
                {
                    _keyIds.Add(new AreasKeyIdModel
                    {
                        KeId = _reader[0].ToString()
                    });
                }
            }
            return _keyIds;
        }

        public async Task<string> GetIdArea(string username)
        {
            var _result = string.Empty;
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { username };
                var _reader = await _context.ExecuteReaderAsync("sp_GetIdAreaByUser", parameters);
                while (_reader.Read())
                {
                    _result = _reader[0].ToString();
                }
            }
            return _result;
        }

        public async Task<List<GetAllQuestionByArea>> GetAllQuestionByArea(int idArea, DataTable _data) {
            var _allQuestion = new List<GetAllQuestionByArea>();
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { idArea, _data };
                var _reader = await _context.ExecuteReaderAsync("sp_GetAllQuestion", parameters);
                while (_reader.Read())
                {
                    _allQuestion.Add(new GetAllQuestionByArea
                    {
                        Question = _reader[0].ToString(),
                        Regions = _reader[1].ToString(),
                        Answer = _reader[2].ToString()
                    });
                }
            }
            return _allQuestion;
        }

        public async Task<List<AreasModel>> ValidGetArea(string mail)
        {
            var listEndPoint = new List<AreasModel>();
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { mail };
                var dReader = await _context.ExecuteReaderAsync("sp_validaUsuarioArea", parameters);
                while (dReader.Read())
                {
                    var area = new AreasModel();
                    area.id = dReader.GetInt32(0);
                    area.area = dReader.GetString(1);

                    listEndPoint.Add(area);
                }
            }
            return listEndPoint;
        }

        #endregion public methods

        #region IDisposable Support

        private bool disposedValue = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
				}

				disposedValue = true;
			}
		}

		#endregion IDisposable Support
	}
}
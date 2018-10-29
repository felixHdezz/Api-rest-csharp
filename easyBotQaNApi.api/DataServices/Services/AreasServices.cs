using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyBotQaNApi.api.Models;
using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using System.Threading.Tasks;

namespace easyBotQaNApi.api.DataServices.Services
{
	public class AreasServices : IAreasServices
	{
		public async Task<List<AreasModel>> getEndPoint()
		{
			var listEndPoint = new List<AreasModel>();
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { };
				var dReader = await _context.ExecuteReader("SP_GETAreas", parameters);
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
				var dReader = await _context.ExecuteReader("sp_GetKeyIdArea", parameters);
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
				return await _context.ExecuteNonQuery("SP_UpdateEndPointTest", parameters);
			}
		}

		public async Task<int> registerUserArea(registerUserAreaModel userArea)
		{
			using (var _context = new DataBaseContext())
			{
				object[] parameters = new object[] { userArea.name, userArea.userName, userArea.email, userArea.idArea };
				return await _context.ExecuteNonQuery("sp_updateUserArea", parameters);
			}
		}

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
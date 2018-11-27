using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace easyBotQaNApi.api.DataServices.Context
{
	internal class DataBaseContext : IDisposable
	{

		private SqlConnection defaultDB = null;

		public DataBaseContext() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = ConfigurationManager.AppSettings["sever"];
			builder.InitialCatalog = ConfigurationManager.AppSettings["database"];
			builder.PersistSecurityInfo = false;
			builder.UserID = ConfigurationManager.AppSettings["userName"];
			builder.Password = ConfigurationManager.AppSettings["password"];
			builder.MultipleActiveResultSets = false;
			builder.Encrypt = true;
			builder.TrustServerCertificate = false;
			builder.ConnectTimeout = 30;

			defaultDB = new SqlConnection(builder.ConnectionString);
			defaultDB.Open();
		}

		public async Task<SqlDataReader> ExecuteReaderAsync(string StoreProcName, params object[] parametersValue) {
			using (var _sCommand = new SqlCommand(StoreProcName, defaultDB))
			{
				_sCommand.CommandType = CommandType.StoredProcedure;
				var _dSet = getParametersStoreProc(StoreProcName);
				if (_dSet.Tables.Count > 0)
				{
                    _sCommand.Parameters.AddRange(setParameters(_dSet, parametersValue));
                    //var table = _dSet.Tables[0];
                    //for (var _i = 0; _i < table.Rows.Count; _i++)
                    //{
                    //	_sCommand.Parameters.AddWithValue(table.Rows[_i][0].ToString(), parametersValue[_i]);
                    //}
                }
				return await _sCommand.ExecuteReaderAsync();
			}
		}

		public async Task<int> ExecuteNonQueryAsync(string strStoreProcName, params object[] parametersValue)
		{
			using (var _sCommand = new SqlCommand(strStoreProcName, defaultDB))
			{
				_sCommand.CommandType = CommandType.StoredProcedure;
                
                var _dSet = getParametersStoreProc(strStoreProcName);                
                if (_dSet.Tables.Count > 0)
				{
                    _sCommand.Parameters.AddRange(setParameters(_dSet, parametersValue));
                    //var table = _dSet.Tables[0];
					//for (var _i = 0; _i < table.Rows.Count; _i++)
					//{
					//	_sCommand.Parameters.AddWithValue(table.Rows[_i][0].ToString(), parametersValue[_i]);
					//}
				}
				return await _sCommand.ExecuteNonQueryAsync();
			}
		}

		#region Pirvate methods

		private DataSet getParametersStoreProc(string StoreProcName) {
			DataSet dataSet = new DataSet();
			string _query = string.Format("EXEC sp_GetParametersStoreProcs '{0}'", StoreProcName);
			using (var _sCommand = new SqlCommand(_query, defaultDB)) {
				_sCommand.CommandType = CommandType.Text;
				var _dTable = new DataTable();
				_dTable.Load(_sCommand.ExecuteReader());
				dataSet.Tables.Add(_dTable);
			}
			return dataSet;
		}

        private SqlParameter[] setParameters(DataSet _dataSet, params object[] _parameters) {
            SqlParameter[] sqlParameters = new SqlParameter[_parameters.Length];
            var table = _dataSet.Tables[0];
            for (var _i = 0; _i < table.Rows.Count; _i++)
            {
                //SqlParameter[] sqlParameters = {
                //    new SqlParameter(table.Rows[_i][0].ToString(), _parameters[_i])
                //};
                //var _sqlParameters = new SqlParameter();
                //_sqlParameters.ParameterName = table.Rows[_i][0].ToString();
                //_sqlParameters.Value = _parameters[_i];
                ////_sqlParametersArray[_i] = _parameters;
                sqlParameters[_i] = new SqlParameter(table.Rows[_i][0].ToString(), _parameters[_i]);
            }

            return sqlParameters;
        }

		#endregion Private methods

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
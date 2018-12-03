using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.Services
{
    public class RegionsService : IRegionsService
    {
        public async Task<List<RegionsModel>> GetRegions()
        {
            List<RegionsModel> regionsModel = new List<RegionsModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var dReader = await _dbContext.ExecuteReaderAsync("sp_GetRegions", parameters);
                while (dReader.Read())
                {
                    regionsModel.Add(new RegionsModel()
                    {
                        Id = Convert.ToInt32(dReader[0]),
                        Region = dReader.GetString(1),
                        Type = dReader.GetString(2),
                        IsActive = Convert.ToInt32(dReader[3])
                    });
                }
            }
            return regionsModel;
        }

        public async Task<List<RegionsModel>> GetRegionsTest()
        {
            List<RegionsModel> regionsModel = new List<RegionsModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var dReader = await _dbContext.ExecuteReaderAsync("sp_GetRegionTest", parameters);
                while (dReader.Read())
                {
                    regionsModel.Add(new RegionsModel()
                    {
                        Id = Convert.ToInt32(dReader[0]),
                        Region = dReader.GetString(1),
                        IsActive = Convert.ToInt32(dReader[2])
                    });
                }
            }
            return regionsModel;
        }

        public async Task<int> UpdateRegion(UpdateRegionModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Id, model.IsActive};
                return  await _dbContext.ExecuteNonQueryAsync("sp_UpdateRegion", parameters);
            }
        }

        public async Task<int> AddRegions(CrudRegionsModel model) {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Id, model.Region, model.IsActive, model.Type, model.IdAreas };
                return await _dbContext.ExecuteNonQueryAsync("sp_AddRegion", parameters);
            }
        }

        public async Task<int> SaveNewRegion(RegionsModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Region, model.IsActive };
                return await _dbContext.ExecuteNonQueryAsync("sp_SaveNewRegion", parameters);
            }
        }

        public async Task<int> SaveNewQuestion(SaveNewQuestion model) {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Question, model.Answer, model.IdArea, model.IdRegions, model.Type };
                return await _dbContext.ExecuteNonQueryAsync("sp_SaveNewQuestion", parameters);
            }
        }
    }
}
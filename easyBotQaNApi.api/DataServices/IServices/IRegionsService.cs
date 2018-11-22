using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.IServices
{
    public interface IRegionsService
    {
        Task<List<RegionsModel>> GetRegions();

        Task<int> UpdateRegion(UpdateRegionModel model);
    }
}
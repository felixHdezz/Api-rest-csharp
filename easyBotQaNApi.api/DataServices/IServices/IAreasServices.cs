﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyBotQaNApi.api.Models;
using easyBotQaNApi.api.DataServices.Services;
using System.Threading.Tasks;
using System.Data;

namespace easyBotQaNApi.api.DataServices.IServices
{
	public interface IAreasServices
	{
		Task<List<AreasModel>> getEndPoint();

		Task<AreaEndPointModel> getAreaById(int id);

		Task<int> updateAreaByuser(int idArea);

		Task<int> registerUserArea(registerUserAreaModel userArea);

        Task<List<AreasKeyIdModel>> GetKeyId(string IdAreas);

        Task<string> GetIdArea(string username);

        Task<List<GetAllQuestionByArea>> GetAllQuestionByArea(int idArea, DataTable _data);

        Task<List<AreasModel>> ValidGetArea(string mail);
    }
}
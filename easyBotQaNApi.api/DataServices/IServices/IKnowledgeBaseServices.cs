using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.IServices
{
	public interface IKnowledgeBaseServices
	{
		Task<int> saveAnswerKnowledge(DataTable _dt, CreateKnowledgeModel model);

		Task<List<QuestionAnswerModel>> GetQuestionAnswersByIdArea(string strArea);

		Task<int> updateAreaKeyId(AreaEndPointModel model);

		Task<int> saveAnswer(SaveAnswerModel model);
	}
}
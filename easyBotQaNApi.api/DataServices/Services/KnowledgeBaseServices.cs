using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.Services
{
	public class KnowledgeBaseServices : IKnowledgeBaseServices
	{
		public async Task<int> saveAnswerKnowledge(DataTable _dTable, CreateKnowledgeModel model)
		{
			using (var _dbContext = new DataBaseContext())
			{
				object[] parameters = new object[] { model.NombreResponsable, model.Apellidos, model.Telefono, model.Email, model.NombreConocimiento, _dTable };
				return await _dbContext.ExecuteNonQuery("sp_CreateKnowledgeBase", parameters);
			}
		}

		public async Task<List<QuestionAnswerModel>> GetQuestionAnswersByIdArea(string strArea) {
			List<QuestionAnswerModel> questionAnswerModel = new List<QuestionAnswerModel>();
			using (var _dbContext = new DataBaseContext())
			{
				object[] parameters = new object[] { strArea };
				var dReader = await _dbContext.ExecuteReader("sp_GetQuestionAnswerByArea", parameters);
				while (dReader.Read())
				{
					var question = new QuestionAnswerModel();
					question.id = 0;
					question.answer = dReader.GetString(1);
					question.source = "Editorial";
					question.questions = dReader.GetString(0).Split(';');
					question.metadata = new string[0];
					questionAnswerModel.Add(question);
				}
			}
			return questionAnswerModel;
		}

		public async Task<int> updateAreaKeyId(AreaEndPointModel model) {
			using (var _dbContext = new DataBaseContext())
			{
				object[] parameters = new object[] { model.KeyId, model.Area };
				return await _dbContext.ExecuteNonQuery("sp_UpdatekeyIdAreaByName", parameters);
			}
		}

		public async Task<int> saveAnswer(SaveAnswerModel model)
		{
			using (var _dbContext = new DataBaseContext())
			{
				object[] parameters = new object[] { model.question, model.answer, model.idArea };
				return await _dbContext.ExecuteNonQuery("sp_SaveAnswer", parameters);
			}
		}
	}
}
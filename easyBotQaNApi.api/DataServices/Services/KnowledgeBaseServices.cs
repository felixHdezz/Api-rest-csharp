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
        public async Task<int> SaveAnswerKnowledge(DataTable _dTable, CreateKnowledgeModel model)
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

        public async Task<int> UpdateAreaKeyId(AreaEndPointModel model) {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.KeyId, model.Area };
                return await _dbContext.ExecuteNonQuery("sp_UpdatekeyIdAreaByName", parameters);
            }
        }

        public async Task<int> SaveAnswer(SaveAnswerModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.question, model.answer, model.idArea };
                return await _dbContext.ExecuteNonQuery("sp_SaveAnswer", parameters);
            }
        }

        public async Task<int> AddQuestion(AddQuestionModel model) {
            using (var _dbContext = new DataBaseContext()) {
                object[] parameters = new object[] { model.IdArea, model.Question, model.Answer, model.Type, model.IsActive };
                return await _dbContext.ExecuteNonQuery("sp_AddQuestion", parameters);
            }
        }

        public async Task<List<EnvironmentsModel>> GetEnvironments() {
            List<EnvironmentsModel> _environments = new List<EnvironmentsModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var _dReader = await _dbContext.ExecuteReader("sp_GetEnvironments", parameters);

                while (_dReader.Read())
                {
                    var environments = new EnvironmentsModel();
                    environments.Id = Convert.ToInt32(_dReader[0]);
                    environments.Environment = _dReader[1].ToString();
                    environments.HostName = _dReader[2].ToString();
                    environments.EndPointKey = _dReader[3].ToString();
                    environments.Username = _dReader[4].ToString();
                    environments.Password = _dReader[5].ToString();
                    environments.IsActive = Convert.ToInt32(_dReader[6]);

                    _environments.Add(environments);
                }
            }
            return _environments;
        }

        public async Task<int> Crud_Environment(EnvironmentsModel model) {
            using (var _dbContext = new DataBaseContext()) {
                object[] parameters = new object[] { model.Id, model.Environment, model.HostName, model.EndPointKey, model.Username, model.Password, model.IsActive };
                return await _dbContext.ExecuteNonQuery("sp_CRUD_Environment", parameters);
            }
        }

        public async Task<int> UpdateStatusEnv(int IdEnv, int Status)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { IdEnv, Status };
                return await _dbContext.ExecuteNonQuery("sp_UpdateStatusEnvironment", parameters);
            }
        }
    }
}
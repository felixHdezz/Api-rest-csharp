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
        public async Task<List<KnowledgeBasesModel>> GetKnowledgesBases() {
            List<KnowledgeBasesModel> _knowledgeBases = new List<KnowledgeBasesModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var dReader = await _dbContext.ExecuteReaderAsync("sp_GetAreasEnv", parameters);
                while (dReader.Read())
                {
                    _knowledgeBases.Add(new KnowledgeBasesModel() {
                        Id =  Convert.ToInt32(dReader[0]),
                        KnowledgeBase = dReader[1].ToString(),
                        Description = dReader[2].ToString(),
                        KeyId = dReader[3].ToString(),
                        Id_contact = Convert.ToInt32(dReader[4]),
                        Contact = dReader[5].ToString(),
                        LastName = dReader[6].ToString(),
                        Email = dReader[7].ToString(),
                        Id_env = Convert.ToInt32(dReader[8]),
                        Environment = dReader[9].ToString()
                    });
                }
            }
            return _knowledgeBases;
        }

        public async Task<int> SaveAnswerKnowledge(DataTable _dTable, CreateKnowledgeModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.NombreResponsable, model.Apellidos, model.Telefono, model.Email, model.NombreConocimiento, _dTable };
                return await _dbContext.ExecuteNonQueryAsync("sp_CreateKnowledgeBase", parameters);
            }
        }

        public async Task<List<QuestionAnswerModel>> GetQuestionAnswersByIdArea(string strArea) {
            List<QuestionAnswerModel> questionAnswerModel = new List<QuestionAnswerModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { strArea };
                var dReader = await _dbContext.ExecuteReaderAsync("sp_GetQuestionAnswerByArea", parameters);
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

        public async Task<int> UpdateKnowledgeBase(KnowledgeBasesModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Id, model.Id_contact, model.Contact, model.LastName, model.Email, model.Id_env };
                return await _dbContext.ExecuteNonQueryAsync("sp_UpdateKnowledgeBase", parameters);
            }
        }

        public async Task<int> DeleteKnowledgeBase(int id)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { id };
                return await _dbContext.ExecuteNonQueryAsync("sp_DeleteKnowledgeBase", parameters);
            }
        }

        public async Task<int> UpdateAreaKeyId(AreaEndPointModel model) {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.KeyId, model.Area };
                return await _dbContext.ExecuteNonQueryAsync("sp_UpdatekeyIdAreaByName", parameters);
            }
        }

        public async Task<int> SaveAnswer(SaveAnswerModel model)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.question, model.answer, model.idArea };
                return await _dbContext.ExecuteNonQueryAsync("sp_SaveAnswer", parameters);
            }
        }

        public async Task<int> AddQuestion(AddQuestionModel model) {
            using (var _dbContext = new DataBaseContext()) {
                object[] parameters = new object[] { model.IdArea, model.Question, model.Answer, model.Type, model.IsActive };
                return await _dbContext.ExecuteNonQueryAsync("sp_AddQuestion", parameters);
            }
        }

        public async Task<List<EnvironmentsModel>> GetEnvironments() {
            List<EnvironmentsModel> _environments = new List<EnvironmentsModel>();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var _dReader = await _dbContext.ExecuteReaderAsync("sp_GetEnvironments", parameters);

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
                return await _dbContext.ExecuteNonQueryAsync("sp_CRUD_Environment", parameters);
            }
        }

        public async Task<int> UpdateStatusEnv(int IdEnv, int Status)
        {
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { IdEnv, Status };
                return await _dbContext.ExecuteNonQueryAsync("sp_UpdateStatusEnvironment", parameters);
            }
        }

        public async Task<EndPointsModel> GetEndPointQaNMaker(string username) {
            var _list_endPoint = new EndPointsModel();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { username };
                var result = await _dbContext.ExecuteReaderAsync("sp_GetEndPointByUser", parameters);
                List<EnviromentEndpointModel> endpoint = new List<EnviromentEndpointModel>();
                while (result.Read())
                {
                    endpoint.Add(new EnviromentEndpointModel
                    {
                        environment = result.GetString(0),
                        qnaAuthKey = result.GetString(1),
                        qnaKBId = result.GetString(2),
                        endpointHostName = result.GetString(3),
                        username = result.GetString(4),
                        password = result.GetString(5)
                    });
                }
                _list_endPoint.endpoints = endpoint;
            }
            return _list_endPoint;
        }

        public async Task<OrganizationUnitModel> GetOrganization(string OrgUnit) {
            var _OrganizationU = new OrganizationUnitModel();
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { OrgUnit };
                var result = await _dbContext.ExecuteReaderAsync("sp_GetDataOrgUnit", parameters);
                while (result.Read())
                {
                    _OrganizationU.Name = result[0].ToString();
                    _OrganizationU.Type = result[1].ToString();
                    _OrganizationU.IsActive = result.GetBoolean(2);
                }
            }
            return _OrganizationU;
        }

        public async Task<string> GetAnswerForUser(GetMessage model) {
            var strMessage = string.Empty;
            using (var _dbContext = new DataBaseContext())
            {
                object[] parameters = new object[] { model.IdQuestion, model.Region, model.IdArea };
                var result = await _dbContext.ExecuteReaderAsync("sp_GetAnswerForUser", parameters);
                while (result.Read())
                {
                    strMessage = result[0].ToString();
                }
            }
            return strMessage;
        }
    }
}
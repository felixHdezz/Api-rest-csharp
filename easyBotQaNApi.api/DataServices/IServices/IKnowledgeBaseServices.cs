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
        Task<int> SaveAnswerKnowledge(DataTable _dt, CreateKnowledgeModel model);

        Task<List<QuestionAnswerModel>> GetQuestionAnswersByIdArea(string strArea);

        Task<int> UpdateAreaKeyId(AreaEndPointModel model);

        Task<int> SaveAnswer(SaveAnswerModel model);

        Task<int> AddQuestion(AddQuestionModel addQuestionModel);

        Task<List<EnvironmentsModel>> GetEnvironments();

    }
}
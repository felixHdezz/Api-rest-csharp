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
        /// <summary>
        /// Crea un base de conocimiento en QnAMaker y guarda la  informacion a la base de datos
        /// </summary>
        /// <param name="_dt"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> SaveAnswerKnowledge(DataTable _dt, CreateKnowledgeModel model);

        /// <summary>
        /// Obtiene la lista de las preguntas y respuestas por area
        /// </summary>
        /// <param name="strArea"></param>
        /// <returns></returns>
        Task<List<QuestionAnswerModel>> GetQuestionAnswersByIdArea(string strArea);

        /// <summary>
        /// Obtiene el end point de una area
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateAreaKeyId(AreaEndPointModel model);

        /// <summary>
        /// Guarda una respuesta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> SaveAnswer(SaveAnswerModel model);

        /// <summary>
        /// Agregar una nueva pregunta por area
        /// </summary>
        /// <param name="addQuestionModel"></param>
        /// <returns></returns>
        Task<int> AddQuestion(AddQuestionModel addQuestionModel);

        /// <summary>
        /// Obtiene los ambientes de desarrollo
        /// </summary>
        /// <returns></returns>
        Task<List<EnvironmentsModel>> GetEnvironments();

        /// <summary>
        /// Guarda o modifica informacion de un ambiente con el end point
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Crud_Environment(EnvironmentsModel model);

        Task<int> UpdateStatusEnv(int IdEnv, int Status);
    }
}
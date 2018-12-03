using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace easyBotQaNApi.api.Controllers
{
    [RoutePrefix("api/regions")]
    public class RegionsController : ApiController
    {
        #region constructor

        public RegionsController(IRegionsService regionService)
        {
            services = regionService;
        }
        #endregion constructor

        public IRegionsService services { get; set; }

        #region methods public

        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get() {
            try
            {
                var result = await services.GetRegionsTest();
                return Ok(result);
            }
            catch (Exception ex) {
                return Ok(ex.Message.ToString());
            }
        }

        [Route("{areas}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string areas)
        {
            //var result = await services.AddRegions(model);
            return Ok();
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> Post(RegionsModel model)
        {
            var result = await services.SaveNewRegion(model);
            return Ok(result);
        }

        [Route("savequestion")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveNewQuestion(RegionsModel model)
        {
            var result = await services.SaveNewRegion(model);
            return Ok(result);
        }

        [Route("savenewquestion")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveNewQuestion(SaveNewQuestion model)
        {
            var result = await services.SaveNewQuestion(model);
            return Ok(result);
        }

        [Route]
        [HttpPut]
        public async Task<IHttpActionResult> Put(UpdateRegionModel model)
        {
            var result = await services.UpdateRegion(model);
            return Ok(result);
        }


        #endregion methods public
    }
}


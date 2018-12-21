using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace easyBotQaNApi.api.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        public UsersController(IUsersService usersService) {
            Services = usersService;
        }

        public IUsersService Services { get; set; }

        [Authorize]
        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get() {
            var result = await Services.GetUsers();
            return Ok(result);
        }

        [Authorize]
        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> Post(SaveUserModel model)
        {
            var result = await Services.SaveUser(model);
            return Ok(result);
        }

        [Authorize]
        [Route]
        [HttpPut]
        public async Task<IHttpActionResult> Put(UpdateUserModel model)
        {
            var result = await Services.UpdateUser(model);
            return Ok(result);
        }
    }
}

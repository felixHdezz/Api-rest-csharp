using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace easyBotQaNApi.api.Security
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class AuthenticateController : ApiController
    {
        #region Public Constructors

        public AuthenticateController(IAuthenticateService _service) {
            Service = _service;
        }

        #endregion Public Constructors

        #region Protected Properties

        protected IAuthenticateService Service { get; set; }

        #endregion  Protected Properties

        #region Public Methods

        [HttpPost]
        [Route("authenticate")]
        public async Task<IHttpActionResult> Authenticate(AuthenticateModel model)
        {
            if (model == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            var isCredentialValid = await Service.CredentialValidate(model);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(model.Username);
                return Ok(token);
            }
            else
            {
                return Ok("Unauthorized");
            }
        }

        #endregion  Public Methods
    }
}

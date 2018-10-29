using easyBotQaNApi.api.Infrastructure.Controllers;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.DataServices.Services;
using easyBotQaNApi.api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Web;

namespace easyBotQaNApi.api.Controllers
{
	[RoutePrefix("api/areas")]
	public class AreasController : ApiController
	{

		public AreasController(IAreasServices _IAreasServices)
		{
			_Services = _IAreasServices;
		}

		private IAreasServices _Services { get; }

		[Route]
		[HttpGet]
		public async Task<IHttpActionResult> Get()
		{
			var result = await _Services.getEndPoint();
			return Ok(result);
		}

		[Route("{id}")]
		[HttpGet]
		public async Task<IHttpActionResult> Get(int id) {
			var result = await _Services.getAreaById(id);
			return Ok(result);
		}

		[Route("updateUserArea/{idArea}")]
		[HttpPost]
		public async Task<IHttpActionResult> updateUserArea(int idArea)
		{
			var userName = HttpContext.Current.User.Identity.Name.ToString();
			var result = await _Services.updateAreaByuser(idArea);

			return Ok(result);
		}

		[Route("registerUserArea")]
		[HttpPost]
		public async Task<IHttpActionResult> registerUserArea(registerUserAreaModel userArea) {
			if (userArea.idArea != 0 && userArea.userName != null && userArea.email != null)
			{
				return Ok(await _Services.registerUserArea(userArea));
			}
			else
			{
				return BadRequest("data invalid");
			}
		}

		[Route("getUserLogon")]
		[HttpPost]
		public IHttpActionResult getUserLogon()
		{
			var userName = HttpContext.Current.User.Identity.Name.ToString();
			return Ok(userName);
		}

		[Route("getUserCurrent")]
		[HttpGet]
		public IHttpActionResult getUserCurrent() {
			var user = HttpContext.Current.User.Identity.Name.ToString();
			return Ok(user);
		}

		[HttpPut]
		public IHttpActionResult Put()
		{
			return Ok();
		}
	}
}

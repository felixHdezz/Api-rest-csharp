using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public async Task<bool> CredentialValidate(AuthenticateModel model) {
            var isCredentialValid = false;
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { model.Username, model.Password };
                var _result = await _context.ExecuteReaderAsync("sp_GetLoginUsuario", parameters);
                if (_result.HasRows) {
                    isCredentialValid = true;
                }
            }
            return isCredentialValid;
        }
    }
}
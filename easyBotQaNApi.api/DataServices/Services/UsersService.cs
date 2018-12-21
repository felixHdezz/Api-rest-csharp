using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyBotQaNApi.api.Models;
using easyBotQaNApi.api.DataServices.Context;
using easyBotQaNApi.api.DataServices.IServices;
using System.Threading.Tasks;

namespace easyBotQaNApi.api.DataServices.Services
{
    public class UsersService : IUsersService
    {
        public async Task<List<UserModel>> GetUsers() {
            var _listUsers = new List<UserModel>();
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { };
                var dReader = await _context.ExecuteReaderAsync("sp_GetUsuarios", parameters);
                while (dReader.Read())
                {
                    _listUsers.Add(new UserModel {
                        Id = Convert.ToInt32(dReader[0]),
                        UserName = dReader[1].ToString(),
                        Password = dReader[2].ToString(),
                        IsActive = Convert.ToInt32(dReader[3])
                    });
                }
            }
            return _listUsers;
        }

        public async Task<int> SaveUser(SaveUserModel model) {
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { model.UserName, model.Password };
                 return await _context.ExecuteNonQueryAsync("sp_SaveNewUser", parameters);
            }
        }

        public async Task<int> UpdateUser(UpdateUserModel model)
        {
            using (var _context = new DataBaseContext())
            {
                object[] parameters = new object[] { model.IdUsuario, model.UserName, model.Password, model.Status };
                return await _context.ExecuteNonQueryAsync("sp_UpdateUsers", parameters);
            }
        }
    }
}
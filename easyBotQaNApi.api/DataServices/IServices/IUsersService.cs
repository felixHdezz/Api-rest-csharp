using easyBotQaNApi.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace easyBotQaNApi.api.DataServices.IServices
{
    public interface IUsersService
    {
        Task<List<UserModel>> GetUsers();

        Task<int> SaveUser(SaveUserModel model);

        Task<int> UpdateUser(UpdateUserModel model);
    }
}
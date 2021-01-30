using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Dtos.Account;
using wordmeister_api.Dtos.General;
using wordmeister_api.Model;

namespace wordmeister_api.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        General.ResponseResult CreateUser(SignUp model);
    }
}

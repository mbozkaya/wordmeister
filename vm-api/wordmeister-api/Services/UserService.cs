using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using wordmeister_api.Dtos;
using wordmeister_api.Dtos.Account;
using wordmeister_api.Dtos.General;
using wordmeister_api.Entities;
using wordmeister_api.Interfaces;

namespace wordmeister_api.Services
{
    public class UserService : IUserService
    {
        private readonly Appsettings _appSettings;
        private WordMeisterDbContext _wordMeisterDbContext;

        public UserService(IOptions<Appsettings> appSettings, WordMeisterDbContext wordMeisterDbContext)
        {
            _appSettings = appSettings.Value;
            _wordMeisterDbContext = wordMeisterDbContext;

        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _wordMeisterDbContext.User.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _wordMeisterDbContext.User.Where(w=>w.Status).ToList();
        }

        public User GetById(int id)
        {
            return _wordMeisterDbContext.User.Where(w=>w.Id==id).FirstOrDefault();
        }

        public General.ResponseResult CreateUser(SignUp model)
        {
            var user = _wordMeisterDbContext.User.Where(w => w.Email == model.Email).FirstOrDefault();

            if(user != null)
            {
                return new General.ResponseResult() { Error = true, ErrorMessage = "There is a user that have same email" };
            }

            _wordMeisterDbContext.User.Add(new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                CreatedDate = DateTime.Now,
                Guid = Guid.NewGuid(),
                Status = true,
            });

            _wordMeisterDbContext.SaveChanges();

            //TODO Send Email

            return new General.ResponseResult();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

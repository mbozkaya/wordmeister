using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using wordmeister_api.Dtos;
using wordmeister_api.Dtos.Account;
using wordmeister_api.Dtos.General;
using wordmeister_api.Entity;
using wordmeister_api.Helpers;
using wordmeister_api.Interfaces;
using wordmeister_api.Model;
using static wordmeister_api.Helpers.Enums;

namespace wordmeister_api.Services
{
    public class UserService : IUserService
    {
        private readonly Appsettings _appSettings;
        private WordmeisterContext _wordMeisterDbContext;
        private readonly string uploadFilePath;


        public UserService(IOptions<Appsettings> appSettings, WordmeisterContext wordMeisterDbContext)
        {
            _appSettings = appSettings.Value;
            _wordMeisterDbContext = wordMeisterDbContext;
            uploadFilePath = "Files";
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _wordMeisterDbContext.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            var ppUri = GetUserPP(user.Id);

            return new AuthenticateResponse(user, token, ppUri);
        }

        public IEnumerable<User> GetAll()
        {
            return _wordMeisterDbContext.Users.Where(w => w.Status).ToList();
        }

        public User GetById(int id)
        {
            return _wordMeisterDbContext.Users.Where(w => w.Id == id).FirstOrDefault();
        }

        public General.ResponseResult CreateUser(SignUp model)
        {
            var user = _wordMeisterDbContext.Users.Where(w => w.Email == model.Email).FirstOrDefault();

            if (user != null)
            {
                return new General.ResponseResult() { Error = true, ErrorMessage = "There is a user that have same email" };
            }

            _wordMeisterDbContext.Users.Add(new User
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

        public General.ResponseResult UploadFiles(List<UploadFileDto.Request> fileModel, int userId)
        {
            var currentUser = GetById(userId);

            List<string> acceptedFileType = new List<string> { ".jpeg", ".png", ".jpg", ".bmp" };
            var isUnAcceptedFile = fileModel.Any(a => !acceptedFileType.Contains(Path.GetExtension(a.File.FileName)));
            if (isUnAcceptedFile)
            {
                return new General.ResponseResult
                {
                    Error = true,
                    ErrorMessage = "Not validating file format was found."
                };
            }

            var filesHasErrors = new List<string>();

            foreach (var item in fileModel)
            {
                if (item.File.Length < 1)
                {
                    continue;
                }

                var result = UploadFile(item, currentUser);

                if (result.error)
                {
                    filesHasErrors.Add(item.File.FileName);
                }
            }

            if (filesHasErrors.Count > 0)
            {
                return new General.ResponseResult
                {
                    Error = true,
                    ErrorMessage = string.Concat(string.Join(Environment.NewLine, filesHasErrors), " The file/files have errors while uploading. "),
                };
            }

            return new General.ResponseResult();
        }

        public General.ResponseResult GetAccountInformation(int userId)
        {
            var user = GetById(userId);

            return new General.ResponseResult
            {
                Data = new AccountResponse.Information
                {
                    Email = user.Email,
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    PictureUri = GetUserPP(userId),
                }
            };
        }

        public string GetUserPP(long userId)
        {
            var uri = _wordMeisterDbContext.UploadFiles
                .Where(w => w.UserId == userId && w.Type == (int)UploadFileType.ProfilePic && w.Status)
                .Select(s => s.Uri)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(uri))
            {
                uri = $"{uploadFilePath}/PP/default.png";
            }

            return $"{uploadFilePath}/{uri}";
        }

        private (bool error, string message) UploadFile(UploadFileDto.Request item, User user)
        {
            var fileGuid = Guid.NewGuid();
            var fileExtension = Path.GetExtension(item.File.FileName);
            var fileUri = $"{item.Type.GetDirectoryName()}/{user.Guid.ToString("N")}";

            var filePath = $"{uploadFilePath}/{fileUri}";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            try
            {
                using (var stream = new FileStream(Path.Combine(filePath, string.Concat(fileGuid, fileExtension)), FileMode.Create))
                {
                    item.File.CopyTo(stream);
                }

                fileUri = $"{fileUri}/{fileGuid}{fileExtension}";

                _wordMeisterDbContext.UploadFiles.Add(new Model.UploadFile
                {
                    CreatedDate = DateTime.Now,
                    Description = item.Description,
                    Extension = fileExtension,
                    Guid = fileGuid,
                    OriginalName = item.File.FileName,
                    Status = true,
                    Type = (int)item.Type,
                    Uri = fileUri,
                    UserId = user.Id,
                });
                _wordMeisterDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new(true, ex.Message);
            }

            return new(false, string.Empty);
        }
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

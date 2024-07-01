using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services.Context;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FormBackend.Services{
    public class UserService{
        private readonly DataContext _context;
        public UserService(DataContext context){_context = context;}
        public bool AddUser(CreateAccountDTO user){
            if(!DoesUserExist(user.Email)){
                PassDTO pass = HashPassword(user.Password);
                UserModel newUser = new(){
                    ID = user.ID,
                    Email = user.Email,
                    Salt = pass.Salt,
                    Hash = pass.Hash,
                    IsAdmin = user.IsAdmin,
                };
                _context.UserInfo.Add(newUser);
            }
            return _context.SaveChanges() != 0;
        }
        public bool DoesUserExist(string email) => _context.UserInfo.SingleOrDefault(u => u.Email == email) != null;
        public UserDTO Converter(UserModel user){
            return new UserDTO{
                ID = user.ID,
                Email = user.Email,
                IsAdmin = user.IsAdmin 
            };
        }
        public UserModel GetUserByUsername(string username) => _context.UserInfo.SingleOrDefault(u => u.Email == username);
        public bool DeleteUser(int id) => _context.UserInfo.Remove((UserModel)_context.UserInfo.Where(u => u.ID == id)) != null && _context.SaveChanges() != 0;
        public IEnumerable<UserDTO> GetUsers(){
            IEnumerable<UserModel> users = _context.UserInfo;
            return users.Select(user => Converter(user)).ToList();
        }
        public PassDTO HashPassword(string passowrd){
            PassDTO newHashPassword = new();
            byte[] SaltByte = new byte[64];
            RNGCryptoServiceProvider provider = new();
            provider.GetNonZeroBytes(SaltByte);
            string salt = Convert.ToBase64String(SaltByte);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new(passowrd, SaltByte, 10000);
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;
            return newHashPassword;
        }
        public IResult Login(LoginDTO user){
            IResult Result = Results.BadRequest("Something went wrong"); 
            if(DoesUserExist(user.Username)){
                UserModel userModel = GetUserByUsername(user.Username);
                PassDTO pass = HashPassword(user.Password);
                if(pass.Hash == userModel.Hash && pass.Salt == userModel.Salt){
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                    var tokenOptions = new JwtSecurityToken(
                        issuer : "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        signingCredentials: signingCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    Result = tokenString == null ? Results.NotFound("There was an error in  the login") : Results.Ok(new {token = tokenString});
                } 
            }
            return Result;
        }

        public bool ResetPassword(ResetPassDTO NewPass){
            UserModel foundUser = GetUserByUsername(NewPass.Email);
            if(foundUser != null){ 
                var UpdatedPass = HashPassword(NewPass.NewPassword);
                if(foundUser.Salt == UpdatedPass.Salt || foundUser.Hash == UpdatedPass.Hash){
                    return false;
                }
                foundUser.Salt = UpdatedPass.Salt;
                foundUser.Hash = UpdatedPass.Hash;
                return true;
            }else return false;
        }
        public bool EditUser(CreateAccountDTO user) => _context.UserInfo.Update((UserModel)_context.UserInfo.Where(u => u.Email == user.Email)) != null && _context.SaveChanges() != 0;
    }
    

}
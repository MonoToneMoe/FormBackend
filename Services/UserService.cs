using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services.Context;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

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

        public UserModel GetUserByUsername(string email) => _context.UserInfo.SingleOrDefault(u => u.Email == email);
        public bool VerifyUsersPassword(string passowrd, string storedHash, string storedSalt){
            byte[] SaltBytes = Convert.FromBase64String(storedSalt);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new(passowrd, SaltBytes, 10000);
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return newHash == storedHash;
        }

        public IResult Login(LoginDTO user){
            IResult Result = Results.Unauthorized();
            if(DoesUserExist(user.Username)){
                UserModel userModel = GetUserByUsername(user.Username);
                if(VerifyUsersPassword(user.Password, userModel.Hash, userModel.Hash)){
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        signingCredentials: signingCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    Result = Results.Ok("Success");
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

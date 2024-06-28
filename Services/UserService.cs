using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Services{
    public class UserService{
        private readonly DataContext _context;
        public UserService(DataContext context){
            _context = context;
        }
        public bool AddUser(UserModel user){
            _context.UserInfo.Add(user);
            return _context.SaveChanges() != 0;
        }

        public UserDTO Converter(UserModel user){
            return new UserDTO{
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = user.Birthday,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                IsAdmin = user.IsAdmin 
            };
        }
        
        public IEnumerable<UserDTO> GetUsers(){
            IEnumerable<UserModel> users = _context.UserInfo;
            return users.Select(user => Converter(user)).ToList();
        }
        
    }

}
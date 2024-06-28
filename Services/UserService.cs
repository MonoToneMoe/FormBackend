using FormBackend.Model;
using FormBackend.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FormBackend.Services{
    public class UserService{
        private readonly DbContext _context;
        public UserService(DataContext context){
            _context = context;
        }
        public bool AddUser(UserModel user){
            _context.Add(user);
            return true;
        }
        
        // public IEnumerable<UserModel> GetUsers(){
        //     return _context;
        // }
    }

}
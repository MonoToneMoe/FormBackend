using FormBackend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FormBackend.Services{
    public class UserService{
        private readonly DbContext _context;
        public UserService(DbContext context){
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
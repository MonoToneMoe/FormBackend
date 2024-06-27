using FormBackend.Models;
using FormBackend.Services.Context;
namespace FormBackend.Services{
    public class UserService(DataContext context)
    {
        private readonly DataContext _context = context;

        public bool AddUser(UserModel NewUser){
            _context.Add(NewUser);
            return _context.SaveChanges() != 0;
        }
        public IEnumerable<UserModel> GetAllUsers(){
            return _context.UserInfo;
        }
    }

}
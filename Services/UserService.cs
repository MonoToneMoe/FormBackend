using FormBackend.Model;
using Microsoft.AspNetCore.Mvc;
namespace FormBackend.Services{
    public class UserService{
        private readonly List<UserModel> users = new();
        public bool AddUser(UserModel user){
            users.Add(user);
            return true;
        }
        public IEnumerable<UserModel> GetUsers() => users;
    }

}
using FormBackend.Model;
namespace FormBackend{
    public class UserService(){

        private readonly List<UserModel> users = new();
        public bool AddUser(UserModel user){
            users.Add(user);
            return true;
        }
        public IEnumerable<UserModel> GetUsers(){
            return users;
        }
    }

}
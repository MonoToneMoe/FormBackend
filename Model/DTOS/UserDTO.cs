namespace FormBackend.Model.DTOS{
    public class UserDTO{
        public int ID {get; set;}
        public string Email {get; set;}
        public bool IsAdmin {get; set;}
        public UserDTO () { }
    }
}
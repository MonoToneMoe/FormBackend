namespace FormBackend{
    public class UserModel{
        public int ID { get; set; }
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string Birthday {get; set;}
        public string Email { get; set; }
        public string Password {get; set;}
        public string? PhoneNumber { get; set;}
        public UserModel(){

        }
    }
}
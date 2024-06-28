namespace FormBackend.Model{
    public class UserModel{
        public int ID { get; set; }
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string Birthday {get; set;}
        public string Email { get; set; }
        public string Salt {get; set;}
        public string Hash {get; set;}
        public string? PhoneNumber { get; set;}
        public string? Address { get; set;}
        public bool IsAdmin { get; set; } = false;
        public UserModel(){

        }
    }
}
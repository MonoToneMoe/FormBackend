namespace FormBackend.Model{
    public class UserModel{
        public int ID { get; set; }
        public string Email { get; set; }
        public string? Salt {get; set;}
        public string? Hash {get; set;}
        public bool IsAdmin { get; set; } = false;
        public UserModel(){ }
    }
}
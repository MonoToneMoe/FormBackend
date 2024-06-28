using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBackend.Model.DTOS
{
    public class CreateAccountDTO
    {
        public int ID {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string? Address {get; set;}
        public string? PhoneNumber {get; set;}
        public string FirstName {get; set;}
        public string LastName  {get; set;}
        public string Birthday {get; set;}
        public bool IsAdmin {get; set;}

        public CreateAccountDTO(){ }
    }
}
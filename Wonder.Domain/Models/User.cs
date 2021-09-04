using System;

namespace Wonder.Domain.Models
{
    public class User: Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
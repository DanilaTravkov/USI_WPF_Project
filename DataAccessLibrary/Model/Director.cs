using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class Director
    {
        public Director(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Role = role;
        }

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Role Role { get; set; }
    }
}

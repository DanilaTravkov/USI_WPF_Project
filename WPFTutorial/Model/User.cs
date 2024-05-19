using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{

    // Update 19.05 migrations are fixed 
    public enum Role { TEACHER, STUDENT, ADMIN }

    public class User
    {
        public User(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role) 
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Role = role;
        }
        [Key] // decorator to make Id a unique field
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth {  get; set; }

        // This field is for determining how the user should be authorized
        public Role Role { get; set; }
    }

}

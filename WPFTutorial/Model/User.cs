using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{

    // WARNING! At this moment migrations do no work as expected, if you want to change this file and run the app you would have to delete .db file in the bin folder as well as the Migartions folder
    public enum Role { TEACHER, STUDENT, ADMIN }

    public class User
    {
        public User() 
        { 

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Student
    {

        public Student(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role)

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
        public DateTime? DateOfBirth { get; set; }

        // This field is for determining how the user should be authorized
        public Role Role { get; set; }
        public int? CourseId { get; set; }

        // relational fields
        public Course Course { get; set; } // TODO: A student can have only one course. Updated by teacher only if that Course was in HasStudentApplied with value true
        // public IDictionary<string, Model.ELevel> LanguageLevel { get; set; } = new Dictionary<string, Model.ELevel>();
        // public IDictionary<Course, bool> HasStudentApplied { get; set; } // If bool value with key Course is set to true, it means the student has applied for that course

    }
}

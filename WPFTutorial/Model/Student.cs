using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Student : User
    {
        public Student(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role)
        : base(name, surname, email, password, gender, dateOfBirth, role)
        {

        }

        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }

        // relational fields
        public Course Course { get; set; } // TODO: A student can have only one course
        // public IDictionary<string, Model.ELevel> LanguageLevel { get; set; } = new Dictionary<string, Model.ELevel>();

    }
}

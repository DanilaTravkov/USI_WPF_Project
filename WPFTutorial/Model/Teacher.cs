using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Teacher : User
    {
        public Teacher(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role)
        : base(name, surname, email, password, gender, dateOfBirth, role)
        {

        }
        [Key]
        public int Id { get; set; }
    }
}

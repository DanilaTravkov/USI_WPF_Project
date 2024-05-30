using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            CourseApplications = new List<CourseApplication>();
        }

        [Key] // decorator to make Id a unique field
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<int> GradedTeacherIds { get; set; } = new List<int>(); // So that a student can't grade one teacher twice

        // This field is for determining how the user should be authorized
        public Role Role { get; set; }
        public int? CourseId { get; set; }

        // relational fields
        public Course Course { get; set; }
        public ICollection<CourseApplication> CourseApplications { get; set; }

        // Exam relationship
        public int? ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}

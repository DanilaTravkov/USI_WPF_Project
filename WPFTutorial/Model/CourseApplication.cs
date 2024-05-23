using System;
using System.ComponentModel.DataAnnotations;

namespace WPFTutorial.Model
{
    public class CourseApplication
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public bool? IsAccepted { get; set; }
        public string? DenialMessage { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}

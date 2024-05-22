using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Notification
    {
        public Teacher Sender { get; set; }
        public Student Reciever { get; set; }
        public bool IsStudentAccepted {  get; set; }
        public string? MessageFromTeacher { get; set; }
        public bool IsCancelledByStudent {  get; set; }
        public string? MessageFromStudent { get; set; }
        public bool StudentProbableCause {  get; set; }

        public Notification() { }
    }
}

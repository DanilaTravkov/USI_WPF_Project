using System.ComponentModel.DataAnnotations;
using WPFTutorial.Model;

public class Teacher
{
    public Teacher(string name, string surname, string email, string password, string gender, DateTime? dateOfBirth, Role role)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Role = role;
        Courses = new List<Course>();  // Initialize the collection
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

    // relational fields
    public ICollection<Course> Courses { get; set; }  // Add this collection
}

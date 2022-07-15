using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class PersonBO
{
    public int Id { get; set; }
    
    public string Name { get; set; } = "EMPTY NAME";
    
    public int Age { get; set; }
}

public class PersonDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "EMPTY NAME";
    public int Age { get; set; }
}

// public class PersonValidator : AbstractValidator<PersonBO>
// {
//     public PersonValidator()
//     {
//         
//     }
// }
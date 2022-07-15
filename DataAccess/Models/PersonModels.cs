using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace DataAccess.Models;

public class PersonBO
{
    public int Id { get; set; } = -1;
    public string Name { get; set; } = "UNSET NAME";
    public int Age { get; set; } = -1;
}

public class PersonDTO
{
    public int Id { get; set; } = -1;
    public string Name { get; set; } = "UNSET NAME";
    public int Age { get; set; } = -1;
}

public class PersonValidator : AbstractValidator<PersonBO>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).Length(0, 50);
        RuleFor(x => x.Age).InclusiveBetween(0, 150);
    }
}

public interface IPersonManager
{
    Task Manage(PersonBO person);
}

public class PersonManager : IPersonManager
{
    private readonly IValidator<PersonBO> _validator;

    public PersonManager(IValidator<PersonBO> validator)
    {
        _validator = validator;
    }
    
    public async Task Manage(PersonBO person)
    {
        await _validator.ValidateAndThrowAsync(person);
    }
}
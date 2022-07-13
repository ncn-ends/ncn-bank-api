using DataAccess.Models;

namespace DataAccess.Placeholder;

public interface IPersonsAccess
{
    Task<IEnumerable<Person>> GetPersons();
}

public class PersonsAccess : IPersonsAccess
{
    private readonly ISqlDataAccess _db;

    public PersonsAccess(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Person>> GetPersons() =>
        _db.LoadData<Person, dynamic>("Persons_GetAll", new { });
}
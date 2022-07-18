using DataAccess.Models;

namespace DataAccess.Access;

public interface IPersonsAccess
{
    Task<IEnumerable<PersonBO>> GetPersons();
}

public class PersonsAccess : IPersonsAccess
{
    private readonly ISqlDataAccess _db;

    public PersonsAccess(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<PersonBO>> GetPersons() =>
        _db.CallUdf<PersonBO, dynamic>("Persons_GetAll", new { });
}
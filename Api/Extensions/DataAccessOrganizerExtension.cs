using DataAccess;
using DataAccess.Access;

namespace Api.Extensions;

public static class DataAccessOrganizerExtension
{
    public static void AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddSingleton<ISetupAccess, SetupAccess>();
        services.AddSingleton<IPersonsAccess, PersonsAccess>();
        services.AddSingleton<IAddressAccess, AddressAccess>();
        services.AddSingleton<IAccountTypeAccess, AccountTypeAccess>();
        services.AddSingleton<IAccountHolderAccess, AccountHolderAccess>();
        services.AddSingleton<IAccountAccess, AccountAccess>();
    }
}
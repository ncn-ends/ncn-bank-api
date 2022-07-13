using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters);

    Task SaveDataUDF<T>(
        string storedProcedure,
        T parameters);

    Task SaveDataSP<T>(
        string storedProcedure,
        T parameters);
}

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;
    

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
        
        /* azure ?? development */
        _connectionString = _config["POSTGRESQLCONNSTR_Default"] 
                            ?? _config.GetConnectionString("Default");
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<T>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveDataUDF<T>(
        string storedProcedure,
        T parameters)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(
            storedProcedure, 
            parameters, 
            commandType: CommandType.StoredProcedure);
    }
    
    
    public async Task SaveDataSP<T>(
        string sql,
        T parameters)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);
        
        connection.Open();

        var p = new DynamicParameters();
        p.AddDynamicParams(parameters);
        await connection.ExecuteAsync(sql, p);
    }
}
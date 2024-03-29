﻿using System.Data;
using System.Diagnostics;
using Dapper;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DataAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> CallUdf<T, U>(
        string storedProcedure,
        U parameters);

    Task<IEnumerable<T>> ExecRawSql<T>(string query);
    Task<IEnumerable<T>> ExecRawSql<T, U>(string query, U parameters);
    Task ExecRawSql(string query);

    Task<IEnumerable<T>> CallUdf<T>(
        string storedProcedure);
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

    public async Task<IEnumerable<T>> CallUdf<T, U>(
        string storedProcedure,
        U parameters)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<T>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> CallUdf<T>(
        string storedProcedure)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<T>(
            storedProcedure,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> ExecRawSql<T>(string query)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<T>(query);
    }

    public async Task<IEnumerable<T>> ExecRawSql<T, U>(string query, U parameters)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        return await connection.QueryAsync<T>(query, param: parameters);
    }

    public async Task ExecRawSql(string query)
    {
        using IDbConnection connection = new NpgsqlConnection(_connectionString);

        await connection.QueryAsync(query);
    }

    // public async Task SaveDataUDF<T>(
    //     string storedProcedure,
    //     T parameters)
    // {
    //     using IDbConnection connection = new NpgsqlConnection(_connectionString);
    //
    //     await connection.ExecuteAsync(
    //         storedProcedure, 
    //         parameters, 
    //         commandType: CommandType.StoredProcedure);
    //     
    // }
    //
    // public async Task SaveDataSP<T>(
    //     string sql,
    //     T parameters)
    // {
    //     using IDbConnection connection = new NpgsqlConnection(_connectionString);
    //     
    //     connection.Open();
    //
    //     var p = new DynamicParameters();
    //     p.AddDynamicParams(parameters);
    //     await connection.ExecuteAsync(sql, p);
    // }
}
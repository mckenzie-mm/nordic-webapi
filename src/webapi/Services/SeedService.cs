using System;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using webapi.Models;

namespace webapi.Services;

public class SeedService
{
    private readonly string _connectionString;
    public SeedService(string DB_CONNECTION_STRING)
    {
        _connectionString = DB_CONNECTION_STRING;
    }

    public async Task Seed()
    {
        Console.WriteLine("called seed");
        var fileName = _connectionString[(_connectionString.LastIndexOf('=') + 1)..];
        if (File.Exists(fileName)) 
        {
            File.Delete(fileName);
        }
        await CreateTables();
        await SeedCategories();  
    }

    private async Task CreateTables() 
    {
        var sql = @"
                    CREATE TABLE categories (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        slug TEXT NOT NULL
                    );";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            using var command = new SqliteCommand(sql, connection);
            await command.ExecuteNonQueryAsync();

            Console.WriteLine("Table 'categories' created successfully.");

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task SeedCategories() 
    {
        var sql = "INSERT INTO categories (id, name, slug) VALUES (@id, @name, @slug)";
        try
        {
            var incoming = new List<Category>();
            using (StreamReader r = new StreamReader("categories.json"))
            {
                string json = r.ReadToEnd();
                incoming = JsonSerializer.Deserialize<List<Category>>(json);
            }

            if (incoming != null && incoming.Count > 0) {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                await connection.ExecuteAsync(sql, incoming);
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

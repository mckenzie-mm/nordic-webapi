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
        await CreateTable(@"
                    CREATE TABLE categories (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL
                    );");
        Console.WriteLine("Categories table created");

        await CreateTable(@"
                    CREATE TABLE products (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        price INTEGER NOT NULL,
                        smallImage TEXT,
                        mediumImage TEXT,
                        largeImage TEXT,
                        slug TEXT NOT NULL,
                        description TEXT,
                        availability INTEGER NOT NULL,
                        category  TEXT NOT NULL
                    );");

        Console.WriteLine("Products table created");
        await SeedCategories();  
        await SeedProducts();  
    }

    private async Task CreateTable(string sql) 
    {
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private async Task SeedCategories() 
    {
        var sql = "INSERT INTO categories (id, name) VALUES (@id, @name)";
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

    private async Task SeedProducts() 
    {
        var sql = @"INSERT INTO products 
                (id,
                name,
                price,
                smallImage,
                mediumImage,
                largeImage,
                slug,
                description,
                availability,
                category) 
            VALUES
                (@id,
                @name,
                @price,
                @smallImage,
                @mediumImage,
                @largeImage,
                @slug,
                @description,
                @availability,
                @category)";
        try
        {
            var incoming = new List<Product>();
            using (StreamReader r = new StreamReader("products.json"))
            {
                string json = r.ReadToEnd();
                incoming = JsonSerializer.Deserialize<List<Product>>(json);
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

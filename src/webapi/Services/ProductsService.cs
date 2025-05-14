using System;
using Dapper;
using Microsoft.Data.Sqlite;
using webapi.Models;

namespace webapi.Services;

public class ProductsService
{
    private readonly string _connectionString;
    public ProductsService(string connection)
    {
        _connectionString = connection;
    }

    public void Create(Product category)
    {
        // store the product in the database
        
    }

    public Product Get(int categoryId)
    {
        // pull the product from the database
        return null;;
    }

    public async Task<IEnumerable<Product>> Get() {
        var sql = "SELECT * FROM products";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql);
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}

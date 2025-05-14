using Dapper;
using Microsoft.Data.Sqlite;
using webapi.Models;

namespace webapi.Services;

public class CategoriesService
{

    private readonly string _connectionString;
    public CategoriesService(string connection)
    {
        _connectionString = connection;
    }

    public void Create(Category category)
    {
        // store the product in the database
        
    }

    public Category? Get(int categoryId)
    {
        // pull the product from the database
        return null;;
    }

    public async Task<IEnumerable<Category>> Get() {
        var sql = "SELECT * FROM categories";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Category>(sql);
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }


}

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

    public async Task<IEnumerable<Product>> GetProduct(string slug) {
        var sql = "SELECT * FROM products WHERE slug=@slug";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql, new { slug });
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<IEnumerable<Product>> findAll(int currentPage, int ITEMS_PER_PAGE) {

        var OFFSET = (currentPage - 1) * ITEMS_PER_PAGE;

        var sql = "SELECT * FROM products ORDER BY id DESC LIMIT @ITEMS_PER_PAGE OFFSET @OFFSET";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql, new { ITEMS_PER_PAGE, OFFSET});
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<IEnumerable<Product>> FindByCategory(string category, int currentPage, int ITEMS_PER_PAGE)
    {
        var OFFSET = (currentPage - 1) * ITEMS_PER_PAGE;

        var sql = @"SELECT * FROM products WHERE category=@category
                ORDER BY id DESC LIMIT @ITEMS_PER_PAGE OFFSET @OFFSET";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql, new {category, ITEMS_PER_PAGE, OFFSET});
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<IEnumerable<Product>> GetSimilar(string category, int id)
    {
        var sql = @"SELECT * FROM products WHERE category=@category AND NOT id=@id
            ORDER BY id DESC LIMIT @LIMIT";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql, new {category, id, LIMIT=4});
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}


/*
    async findName(name: string) {
            const sql = `SELECT DISTINCT name
                        FROM products
                        WHERE UPPER(name) LIKE UPPER('%${name}%')`;
            const db = await openDb();
            const res = await db.all(sql);
            return res;
        }
*/
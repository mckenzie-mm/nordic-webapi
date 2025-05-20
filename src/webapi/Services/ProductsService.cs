
using Dapper;
using Microsoft.Data.Sqlite;
using webapi.Domain;

namespace webapi.Services;

public class ProductsService
{
    private readonly string _connectionString;
    public ProductsService(string connection)
    {
        _connectionString = connection;
    }

    public void Create(Product product)
    {
        // store the product in the database

    }

    public async Task<int> UpdateAsync(int id, Product product)
    {
        var sql = @"UPDATE products SET 
            name=@name, 
            price=@price,
            description=@description,
            smallImage=@smallImage,
            mediumImage=@mediumImage,
            largeImage=@largeImage,
            slug=@slug,
            availability=@availability
            WHERE id=@id";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var res = await connection.ExecuteAsync(sql, new 
            {
                product.name,
                product.price,
                product.description,
                product.smallImage,
                product.mediumImage,
                product.largeImage,
                product.slug,
                product.availability,
                id
            });
            return res;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }
    public async Task<Product> Get(int id)
    {
        var sql = "SELECT * FROM products WHERE id=@id";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var res = await connection.QuerySingleAsync<Product>(sql, new { id });
            return res;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Product>> Get()
    {
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

    public async Task<Product> GetProduct(string slug)
    {
        var sql = "SELECT * FROM products WHERE slug=@slug";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QuerySingleAsync<Product>(sql, new { slug });

            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Product>> findAll(int currentPage, int ITEMS_PER_PAGE)
    {

        var OFFSET = (currentPage - 1) * ITEMS_PER_PAGE;

        var sql = "SELECT * FROM products ORDER BY id DESC LIMIT @ITEMS_PER_PAGE OFFSET @OFFSET";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var res = await connection.QueryAsync<Product>(sql, new { ITEMS_PER_PAGE, OFFSET });
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

            var res = await connection.QueryAsync<Product>(sql, new { category, ITEMS_PER_PAGE, OFFSET });
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

            var res = await connection.QueryAsync<Product>(sql, new { category, id, LIMIT = 4 });
            return res;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<int> GetCount()
    {
        var sql = "SELECT COUNT(*) FROM products";
        try
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            int count = (int)(Int64)await command.ExecuteScalarAsync();
            Console.WriteLine($"The number of products is {count}");
            return count;

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }
    }
    
    
}


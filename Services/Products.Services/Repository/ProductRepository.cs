using Dapper;
using Microsoft.Data.SqlClient;
using Produts.Services.Models;
using System.Data;

namespace Produts.Services.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnection DbConnection;

    public ProductRepository(string connectionString)
    {
        DbConnection = new SqlConnection(connectionString);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        string query = @"SELECT *                        
                         FROM
                         dbo.Products";
        return await DbConnection.QueryAsync<Product>(query);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        string query = @"SELECT *                        
                         FROM
                         dbo.Products
                         WHERE ProductID = @id";
        return await DbConnection.QueryFirstOrDefaultAsync<Product>(query, new { id = id });
    }

    public async Task<int> AddProductAsync(Product product)
    {
        string query = @"INSERT INTO dbo.Products
                         (ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,Discontinued)
                         VALUES
                         (@ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitsInStock,@UnitsOnOrder,@Discontinued)
                         SELECT CAST(SCOPE_IDENTITY() AS INT)";

        return await DbConnection.QuerySingleAsync<int>(query,product);
    }

    public Task<int> UpdateProductAsync(Product product)
    {
        string query = @"UPDATE dbo.Products
                         SET ProductName  = @ProductName,
                        	SupplierID = @SupplierID,
                        	CategoryID = @CategoryID,
                        	QuantityPerUnit = @QuantityPerUnit,
                        	UnitPrice = @UnitPrice,
                        	UnitsInStock = @UnitsInStock,
                        	UnitsOnOrder = @UnitsOnOrder,
                        	ReorderLevel = @ReorderLevel,
                        	Discontinued = @Discontinued
                         WHERE ProductID = @ProductID";
        return DbConnection.ExecuteAsync(query, product);
    }

    public Task<int> DeleteProductAsync(int id)
    {
        string query = @"DELETE dbo.Products
                         WHERE ProductID = @id";
        return DbConnection.ExecuteAsync(query, new { id = id });
    }
}
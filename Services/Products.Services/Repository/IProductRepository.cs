using Produts.Services.Models;

namespace Produts.Services.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<int> AddProductAsync(Product product);
    Task<int> UpdateProductAsync(Product product);
    Task<int> DeleteProductAsync(int id);
}

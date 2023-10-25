using Produts.Services.Models.Dtos;

namespace Produts.Services.Models;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int SupplierID { get; set; }
    public int CategoryID { get; set; }
    public string QuantityPerUnit { get; set; }
    public double UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
    public int Discontinued { get; set; }
    public int ReorderLevel { get; set; }

    public ProductDto GetProductDto()
    {
        return new ProductDto()
        {
            CategoryId = CategoryID,
            Id = ProductID,
            IsDiscontinued = Discontinued > 0,
            Name = ProductName,
            Price = UnitPrice,
            QtyPerUnit = QuantityPerUnit,
            SupplierId = SupplierID,
            UnitsInStocks = UnitsInStock,
            UnitsOnOrder = UnitsOnOrder,
            ReOrderLevel = ReorderLevel,
        };
    }
}
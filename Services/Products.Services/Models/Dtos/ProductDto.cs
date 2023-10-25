namespace Produts.Services.Models.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public int CategoryId { get; set; }
    public string QtyPerUnit { get; set; }
    public int UnitsInStocks { get; set; }
    public int UnitsOnOrder { get; set; }
    public bool IsDiscontinued { get; set; }
    public double Price { get; set; }
    public int ReOrderLevel { get; set; }

    public Product GetProdut()
    {
        return new Product
        {
            ProductName = Name,
            QuantityPerUnit = QtyPerUnit,
            UnitsOnOrder = UnitsOnOrder,
            CategoryID = CategoryId,
            Discontinued = IsDiscontinued ? 0 : 1,
            SupplierID = SupplierId,
            UnitPrice = Price,
            UnitsInStock = UnitsInStocks,
            ReorderLevel = ReOrderLevel,
            ProductID = Id
        };
    }
}
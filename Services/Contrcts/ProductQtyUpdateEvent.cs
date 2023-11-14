namespace Contrcts;

public class ProductQtyUpdateEvent
{
    public List<ProductToBeUpdated>? Products { get; set; }
}
public class ProductToBeUpdated
{
    public int ProductId { get; set; }
    public decimal ProductQty { get; set; }
}

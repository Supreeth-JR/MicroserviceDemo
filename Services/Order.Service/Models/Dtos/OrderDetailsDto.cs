namespace Orders.Service.Models.Dtos;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
}

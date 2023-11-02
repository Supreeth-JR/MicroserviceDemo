namespace Orders.Service.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string? CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public ShipmentDetailsDto? ShipmentDetails { get; set; }
    public virtual List<OrderDetailsDto>? OrderDetails { get; set; }
}



namespace Orders.Service.Models.Dtos;

public class ShipmentDetailsDto
{
    public int? Via { get; set; }
    public decimal? Freight { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}

namespace Produts.Services.Models.Dtos;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string? ResponseMessage { get; set; }
    public object? Response { get; set; }
}
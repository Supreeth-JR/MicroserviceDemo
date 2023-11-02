using MediatR;
using Orders.Service.Models.Dtos;

namespace Orders.Service.Queries;

public class GetAllOrdersQuery : IRequest<ResponseDto>
{
    public GetAllOrdersQuery(int pageSize,int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
    public int PageNumber { get;}
    public int PageSize { get;} 
}

namespace Application.Commands.OrderDetail.DTOs;

public class ResponseGetPagedOrderDetailDto
{
    public required int Quantity { get; set; }
    public required float UnitPrice { get; set; }
}

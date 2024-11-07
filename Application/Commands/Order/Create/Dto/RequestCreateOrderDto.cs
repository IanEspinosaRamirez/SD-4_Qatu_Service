namespace Application.Commands.Order.Create.Dto;

public class RequestCreateOrderDto {
  public required float TotalPrice { get; set; }
  public required string ShippingMethod { get; set; }
  public required string PaymentMethod { get; set; }
}

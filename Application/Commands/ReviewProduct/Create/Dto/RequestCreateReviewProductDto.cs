namespace Application.Commands.ReviewProduct.Create.Dto;

public class RequestCreateReviewProductDto {
  public required int rating { get; set; }
  public required string content { get; set; }
  public required string productId { get; set; }
}

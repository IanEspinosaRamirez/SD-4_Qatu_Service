namespace Application.Querys.ReviewProduct.DTOs;

public class ResponseGetPagedReviewProductDto {
  public required string Id { get; set; }
  public required int rating { get; set; }
  public required string content { get; set; }
  public required DateTime CreateAt { get; set; }
  public required string UserId { get; set; }
  public required string ProductId { get; set; }
}

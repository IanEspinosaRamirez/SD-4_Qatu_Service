namespace Application.Commands.ReviewProduct.DTOs;

public class ResponseGetReviewProductByIdDto {
  public required int Rating { get; set; }
  public required string Content { get; set; }
  public required DateTime CreateAt { get; set; }
}

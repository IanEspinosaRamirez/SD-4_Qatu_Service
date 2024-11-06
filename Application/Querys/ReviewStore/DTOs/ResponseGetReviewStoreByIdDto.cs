namespace Application.Querys.ReviewStore.DTOs;

public class ResponseGetReviewStoreByIdDto {
  public required int Rating { get; set; }
  public required string Content { get; set; }
  public required DateTime CreateAt { get; set; }
}

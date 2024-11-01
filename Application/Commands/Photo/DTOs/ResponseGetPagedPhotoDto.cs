namespace Application.Commands.Photo.DTOs;

public class ResponseGetPagedPhotoDto {
  public required string Id { get; set; }
  public required string ImageUrl { get; set; }
  public required string ProductId { get; set; }
  public required string StoreId { get; set; }
}

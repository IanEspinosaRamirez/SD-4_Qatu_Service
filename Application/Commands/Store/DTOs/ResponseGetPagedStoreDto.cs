namespace Application.Commands.Store.DTOs;

public class ResponseGetPagedStoreDto {
  public required string Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string Address { get; set; }
  public required string UserId { get; set; }
  public required DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}

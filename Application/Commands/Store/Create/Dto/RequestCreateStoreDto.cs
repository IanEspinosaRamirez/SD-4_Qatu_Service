namespace Application.Commands.Store.Create.Dto;

public class RequestCreateStoreDto {
  public required string Name { get; set; }
  public required string Description { get; set; }
  public required string Address { get; set; }
}

namespace Application.Querys.Prouduct.DTOs;

public class ResponseGetProductByIdDto {
  public required string Name { get; set; }
  public required float Price { get; set; }
  public required string Description { get; set; }
  public required int Stock { get; set; }
  public required string Brand { get; set; }
  public required DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}

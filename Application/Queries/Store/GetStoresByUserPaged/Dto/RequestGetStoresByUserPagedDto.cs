namespace Application.Queries.Store.Dto;

public class RequestGetStoresByUserPagedDto {
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string? FilterField { get; set; }
  public string? FilterValue { get; set; }
  public string? OrderByField { get; set; }
  public bool Ascending { get; set; } = true;
}

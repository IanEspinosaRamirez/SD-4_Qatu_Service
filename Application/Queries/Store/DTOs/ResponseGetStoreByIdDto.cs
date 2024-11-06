namespace Application.Queries.Store.GetById;

public class ResponseGetStoreByIdDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public required DateTime CreatedAt { get; set; }
}

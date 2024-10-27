using Domain.Entities.Users.Enums;

namespace Application.Commands.User.DTOs;

public class ResponseGetPagedUserDto {
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public string? Phone { get; set; }
  public required string Username { get; set; }
  public required string Country { get; set; }
  public required DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public required string Address { get; set; }
  public string? ImageURL { get; set; }
  public string RoleUser { get; set; }
  public bool VerifiedAccount { get; set; }
}

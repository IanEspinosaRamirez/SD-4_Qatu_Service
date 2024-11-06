using System.Linq.Expressions;
using Application.Queries.User.DTOs;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Queries.User.GetPaged;

internal sealed class GetUsersPagedQueryHandler
    : IRequestHandler<GetUsersPagedQuery,
                      ErrorOr<List<ResponseGetPagedUserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersPagedQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork =
            unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<ResponseGetPagedUserDto>>>
    Handle(GetUsersPagedQuery query, CancellationToken cancellationToken)
    {
        var orderByExpression =
            GetOrderByExpression<Domain.Entities.Users.User>(query.OrderByField);

        var users = await _unitOfWork.UserRepository.GetPaged(
            query.PageNumber, query.PageSize, query.FilterField, query.FilterValue,
            orderByExpression, query.Ascending);

        if (!users.Any())
        {
            return Error.Failure("User.NoRecords", "No users found.");
        }

        var userDtos =
            users
                .Select(user => new ResponseGetPagedUserDto
                {
                    Id = user.Id.Value.ToString(),
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Username = user.Username,
                    Country = user.Country,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    Address = user.Address,
                    ImageURL = user.ImageURL,
                    RoleUser = user.RoleUser.ToString(),
                    VerifiedAccount = user.VerifiedAccount
                })
                .ToList();

        return userDtos;
    }

    private Expression<Func<T, object>>? GetOrderByExpression<T>(
        string? orderByField)
    {
        if (string.IsNullOrEmpty(orderByField))
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, orderByField);
        var conversion = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}

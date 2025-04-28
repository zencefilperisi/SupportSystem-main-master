using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.User.Queries
{
    public class GetUsersByRoleQuery : IRequest<List<UserDto>>
    {
        public string Role { get; set; }

        public GetUsersByRoleQuery(string role)
        {
            Role = role;
        }
    }
}

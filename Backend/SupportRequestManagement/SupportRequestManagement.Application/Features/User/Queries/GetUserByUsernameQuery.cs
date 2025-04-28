using MediatR;
using System;

namespace SupportRequestManagement.Application.Features.User.Queries
{
    public class GetUserByUsernameQuery : IRequest<Dtos.UserDto>
    {
        public string Username { get; set; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
} 
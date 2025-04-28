using MediatR;
using SupportRequestManagement.Application.Contracts.Persistence;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Application.Features.User.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByRoleQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByRoleAsync(request.Role);

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();

            return userDtos;
        }
    }
}

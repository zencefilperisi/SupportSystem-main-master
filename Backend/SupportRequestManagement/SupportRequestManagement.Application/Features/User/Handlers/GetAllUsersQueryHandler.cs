using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Application.Features.User.Queries;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            // Add pagination logic if needed, similar to GetSupportRequestsByUserQueryHandler
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}

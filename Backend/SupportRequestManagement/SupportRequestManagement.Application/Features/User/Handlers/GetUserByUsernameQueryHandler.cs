using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Application.Features.User.Queries;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // Required for KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                // Depending on use case, you might return null instead of throwing
                throw new KeyNotFoundException($"User with username '{request.Username}' not found."); // Replace with custom NotFoundException
            }
            return _mapper.Map<UserDto>(user);
        }
    }
} 
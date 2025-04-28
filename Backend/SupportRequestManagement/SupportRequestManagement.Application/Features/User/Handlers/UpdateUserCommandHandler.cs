using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.User.Commands;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // For KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(request.Id);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException($"User with ID '{request.Id}' not found."); // Replace with custom NotFoundException
            }

            // Check if the new email is already taken by another user
            if (userToUpdate.Email != request.Email)
            {
                var existingUserByEmail = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUserByEmail != null && existingUserByEmail.Id != request.Id)
                {
                    throw new Exception($"Email '{request.Email}' is already registered by another user."); // Use custom ValidationException
                }
            }

            // Map updated fields (Email, Role) from command to the existing entity
            // Username, PasswordHash, CreatedAt are ignored based on MappingProfile config
            _mapper.Map(request, userToUpdate);

            await _userRepository.UpdateAsync(userToUpdate);

            // Return the updated user details
            return _mapper.Map<UserDto>(userToUpdate);
        }
    }
}

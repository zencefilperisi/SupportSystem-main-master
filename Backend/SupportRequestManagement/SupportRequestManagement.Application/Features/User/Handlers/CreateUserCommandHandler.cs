using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.User.Commands;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Domain.Entities;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
// using System.Security.Cryptography; // Example using built-in crypto (more complex)
// using Microsoft.AspNetCore.Identity; // Recommended: Use ASP.NET Core Identity for hashing

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        // private readonly IPasswordHasher<User> _passwordHasher; // Inject if using ASP.NET Core Identity

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper /*, IPasswordHasher<User> passwordHasher*/)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
           // _passwordHasher = passwordHasher; // Inject if using ASP.NET Core Identity
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if username or email already exists
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUserByUsername != null)
            {
                throw new Exception($"Username '{request.Username}' is already taken."); // Use custom ValidationException
            }

            var existingUserByEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUserByEmail != null)
            {
                throw new Exception($"Email '{request.Email}' is already registered."); // Use custom ValidationException
            }

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.CreatedAt = DateTime.UtcNow;

            // --- IMPORTANT: Secure Password Hashing Required --- 
            // The user's password needs to be securely hashed before saving.
            // DO NOT store plain text passwords.
            // Consider using ASP.NET Core Identity's IPasswordHasher or a robust library.
            // Example placeholder (MUST BE REPLACED):
            // user.PasswordHash = HashPasswordSecurely(request.Password); 
            user.PasswordHash = "PLACEHOLDER_HASH"; // <<< REPLACE THIS LINE WITH ACTUAL HASHING
            // ----------------------------------------------------

            await _userRepository.AddAsync(user);

            // Don't return the password hash in the DTO
            return _mapper.Map<UserDto>(user);
        }

        // private string HashPasswordSecurely(string password)
        // {
        //     // Implement secure hashing logic here (e.g., using System.Security.Cryptography or ASP.NET Core Identity)
        //     throw new NotImplementedException("Secure password hashing must be implemented.");
        // }
    }
}

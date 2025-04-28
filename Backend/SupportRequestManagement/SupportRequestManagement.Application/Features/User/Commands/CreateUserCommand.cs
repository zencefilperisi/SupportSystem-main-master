using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Domain.Enums; // Required for UserRole
using System.ComponentModel.DataAnnotations;

namespace SupportRequestManagement.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)] // Example password policy
        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User; // Default role
    }
}

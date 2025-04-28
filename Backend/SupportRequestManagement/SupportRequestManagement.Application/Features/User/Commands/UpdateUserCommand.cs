using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using SupportRequestManagement.Domain.Enums; // Required for UserRole
using System.ComponentModel.DataAnnotations;

namespace SupportRequestManagement.Application.Features.User.Commands
{
    // Note: Typically, you wouldn't allow updating Username.
    // Password updates should likely be a separate, dedicated command/process.
    public class UpdateUserCommand : IRequest<UserDto> // Return DTO or Unit
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        // Consider if Role should be updatable and add appropriate authorization checks
        public UserRole Role { get; set; }

        // Password is intentionally omitted - use a separate ChangePasswordCommand
        // public string? NewPassword { get; set; } // Optional: Add if password change is allowed here (not recommended)
    }
}

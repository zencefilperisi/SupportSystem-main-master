using MediatR;
using SupportRequestManagement.Application.Features.User.Commands;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // For KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.User.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetByIdAsync(request.Id);

            if (userToDelete == null)
            {
                // Option 1: Throw if not found
                 throw new KeyNotFoundException($"User with ID '{request.Id}' not found."); // Replace with custom NotFoundException
                
                // Option 2: Silently return if not found (idempotent delete)
                // return Unit.Value;
            }

            // Optional: Add checks here before deleting.
            // For example, prevent deleting the last super admin, 
            // or reassign associated SupportRequests if the user is an admin.
            // if (userToDelete.Role == Domain.Enums.UserRole.SuperAdmin) { /* check count */ }

            await _userRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}

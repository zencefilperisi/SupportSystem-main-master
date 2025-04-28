using MediatR;
using System;

namespace SupportRequestManagement.Application.Features.User.Commands
{
    // Belirli bir kullanıcıyı silmek için kullanılan komut
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}

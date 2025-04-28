using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SupportRequestManagement.Application.Features.SupportType.Commands
{
    public class DeleteSupportTypeCommand : IRequest<Unit> // Returns Unit as we don't need data back
    {
        [Required]
        public int Id { get; set; }

        public DeleteSupportTypeCommand(int id)
        {
             Id = id;
        }
    }
}

using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SupportRequestManagement.Application.Features.SupportType.Commands
{
    public class CreateSupportTypeCommand : IRequest<SupportTypeDto>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true; // Default to active
    }
}

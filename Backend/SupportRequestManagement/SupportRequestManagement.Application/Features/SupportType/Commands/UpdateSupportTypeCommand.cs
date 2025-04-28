using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SupportRequestManagement.Application.Features.SupportType.Commands
{
    public class UpdateSupportTypeCommand : IRequest<SupportTypeDto> // Consider returning Unit or bool if no data needed
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}

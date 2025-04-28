using MediatR;
using SupportRequestManagement.Application.Features.SupportRequestComment.Dtos;
using System;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Commands
{
    public class CreateSupportRequestCommentCommand : IRequest<SupportRequestCommentDto>
    {
        public int SupportRequestId { get; set; }
        public int UserId { get; set; } // User submitting the comment
        public string Comment { get; set; }
        public bool IsAdminComment { get; set; } // Flag if comment is by an admin
    }
}

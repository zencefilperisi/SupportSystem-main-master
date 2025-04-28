using MediatR;
using SupportRequestManagement.Application.Features.SupportRequestComment.Dtos;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Queries
{
    public class GetCommentsBySupportRequestIdQuery : IRequest<List<SupportRequestCommentDto>>
    {
        public int SupportRequestId { get; set; }
        // Optional: Add pagination parameters if needed
        // public int PageNumber { get; set; } = 1;
        // public int PageSize { get; set; } = 10;

        public GetCommentsBySupportRequestIdQuery(int supportRequestId)
        {
            SupportRequestId = supportRequestId;
        }
    }
} 
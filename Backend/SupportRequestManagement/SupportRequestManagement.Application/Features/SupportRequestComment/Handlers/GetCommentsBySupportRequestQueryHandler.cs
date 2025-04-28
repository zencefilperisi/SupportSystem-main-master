using MediatR;
using SupportRequestManagement.Application.Contracts.Persistence;
using SupportRequestManagement.Application.Features.SupportRequestComment.Queries;
using SupportRequestManagement.Application.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Handlers
{
    public class GetCommentsBySupportRequestQueryHandler : IRequestHandler<GetCommentsBySupportRequestQuery, List<CommentDto>>
    {
        private readonly ISupportRequestCommentRepository _commentRepository;

        public GetCommentsBySupportRequestQueryHandler(ISupportRequestCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentDto>> Handle(GetCommentsBySupportRequestQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetCommentsBySupportRequestIdAsync(request.SupportRequestId);
            // İstersen AutoMapper kullanarak dönüşüm yapabilirsin.
            var commentDtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                SupportRequestId = c.SupportRequestId,
                CommentText = c.CommentText,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            }).ToList();

            return commentDtos;
        }
    }
}

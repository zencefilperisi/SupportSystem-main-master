using MediatR;
using SupportRequestManagement.Application.Models;
using System;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Queries
{
    // Bu sorgu, belirli bir destek talebine ait yorumları döndürür
    public class GetCommentsBySupportRequestQuery : IRequest<List<CommentDto>>
    {
        public Guid SupportRequestId { get; set; }

        public GetCommentsBySupportRequestQuery(Guid supportRequestId)
        {
            SupportRequestId = supportRequestId;
        }
    }
}

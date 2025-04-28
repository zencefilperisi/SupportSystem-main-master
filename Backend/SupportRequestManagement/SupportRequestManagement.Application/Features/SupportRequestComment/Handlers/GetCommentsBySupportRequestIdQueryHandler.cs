using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportRequestComment.Dtos;
using SupportRequestManagement.Application.Features.SupportRequestComment.Queries;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Handlers
{
    public class GetCommentsBySupportRequestIdQueryHandler : IRequestHandler<GetCommentsBySupportRequestIdQuery, List<SupportRequestCommentDto>>
    {
        private readonly ISupportRequestCommentRepository _commentRepository;
        private readonly IUserRepository _userRepository; // To get usernames
        private readonly IMapper _mapper;

        public GetCommentsBySupportRequestIdQueryHandler(
            ISupportRequestCommentRepository commentRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<SupportRequestCommentDto>> Handle(GetCommentsBySupportRequestIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetBySupportRequestIdAsync(request.SupportRequestId);

            if (comments == null || !comments.Any())
            {
                return new List<SupportRequestCommentDto>(); // Return empty list if no comments
            }

            // Efficiently get all unique user IDs from the comments
            var userIds = comments.Select(c => c.UserId).Distinct().ToList();

            // Fetch all relevant users in a single query
            var users = (await _userRepository.GetAllAsync()) // Assuming GetAllAsync exists, or use a GetByIdsAsync if available
                        .Where(u => userIds.Contains(u.Id))
                        .ToDictionary(u => u.Id);

            // Map comments to DTOs and enrich with usernames
            var commentDtos = comments.Select(comment =>
            {
                var dto = _mapper.Map<SupportRequestCommentDto>(comment);
                if (users.TryGetValue(comment.UserId, out var user))
                {
                    dto.Username = user.Username;
                }
                else
                {
                    dto.Username = "Unknown User"; // Handle case where user might be deleted
                }
                return dto;
            }).ToList();

            // Optional: Apply pagination if added to the query
            // commentDtos = commentDtos.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            return commentDtos;
        }
    }
} 
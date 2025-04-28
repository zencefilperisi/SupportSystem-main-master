using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportRequestComment.Commands;
using SupportRequestManagement.Application.Features.SupportRequestComment.Dtos;
using SupportRequestManagement.Domain.Entities;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportRequestComment.Handlers
{
    public class CreateSupportRequestCommentCommandHandler : IRequestHandler<CreateSupportRequestCommentCommand, SupportRequestCommentDto>
    {
        private readonly ISupportRequestCommentRepository _commentRepository;
        private readonly ISupportRequestRepository _supportRequestRepository; // To check if request exists
        private readonly IUserRepository _userRepository; // To check if user exists and get username
        private readonly IMapper _mapper;

        public CreateSupportRequestCommentCommandHandler(
            ISupportRequestCommentRepository commentRepository,
            ISupportRequestRepository supportRequestRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _supportRequestRepository = supportRequestRepository ?? throw new ArgumentNullException(nameof(supportRequestRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SupportRequestCommentDto> Handle(CreateSupportRequestCommentCommand request, CancellationToken cancellationToken)
        {
            // Validate SupportRequest exists
            var supportRequest = await _supportRequestRepository.GetByIdAsync(request.SupportRequestId);
            if (supportRequest == null)
            {
                throw new Exception($"SupportRequest with ID {request.SupportRequestId} not found."); // Use custom exception
            }

            // Validate User exists
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception($"User with ID {request.UserId} not found."); // Use custom exception
            }

            var comment = _mapper.Map<Domain.Entities.SupportRequestComment>(request);
            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);

            // Map to DTO, including the username
            var commentDto = _mapper.Map<SupportRequestCommentDto>(comment);
            commentDto.Username = user.Username; // Add username to DTO

            // Optional: Create a notification for relevant parties (e.g., request owner, assigned admin)
            // var notification = new Notification { ... };
            // await _notificationRepository.AddAsync(notification);

            return commentDto;
        }
    }
}

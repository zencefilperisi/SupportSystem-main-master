using MediatR;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // For KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;
using SupportRequestManagement.Application.Features.SupportType.Commands;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class DeleteSupportTypeCommandHandler : IRequestHandler<DeleteSupportTypeCommand, Unit>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;

        public DeleteSupportTypeCommandHandler(ISupportTypeRepository supportTypeRepository)
        {
            _supportTypeRepository = supportTypeRepository ?? throw new ArgumentNullException(nameof(supportTypeRepository));
        }

        public async Task<Unit> Handle(DeleteSupportTypeCommand request, CancellationToken cancellationToken)
        {
            var supportTypeToDelete = await _supportTypeRepository.GetByIdAsync(request.Id);

            if (supportTypeToDelete == null)
            {
                // Option 1: Throw an exception if the type doesn't exist
                throw new KeyNotFoundException($"SupportType with ID '{request.Id}' not found."); // Replace with custom NotFoundException

                // Option 2: Return silently if non-existence is acceptable
                // return Unit.Value;
            }

            // Optional: Add logic to check if this SupportType is currently in use by any SupportRequests
            // If it is, you might want to prevent deletion or handle it differently (e.g., soft delete by setting IsActive=false)
            // var relatedRequests = await _supportRequestRepository.GetBySupportTypeIdAsync(request.Id);
            // if (relatedRequests.Any())
            // {
            //    throw new InvalidOperationException("Cannot delete SupportType as it is currently in use.");
            // }

            await _supportTypeRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}

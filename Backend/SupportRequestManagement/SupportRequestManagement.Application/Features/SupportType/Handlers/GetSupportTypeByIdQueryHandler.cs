using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using SupportRequestManagement.Application.Features.SupportType.Queries;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // For KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class GetSupportTypeByIdQueryHandler : IRequestHandler<GetSupportTypeByIdQuery, SupportTypeDto>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly IMapper _mapper;

        public GetSupportTypeByIdQueryHandler(ISupportTypeRepository supportTypeRepository, IMapper mapper)
        {
            _supportTypeRepository = supportTypeRepository ?? throw new ArgumentNullException(nameof(supportTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SupportTypeDto> Handle(GetSupportTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var supportType = await _supportTypeRepository.GetByIdAsync(request.Id);

            if (supportType == null)
            {
                throw new KeyNotFoundException($"SupportType with ID '{request.Id}' not found."); // Replace with custom NotFoundException
            }

            return _mapper.Map<SupportTypeDto>(supportType);
        }
    }
}

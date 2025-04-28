using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using SupportRequestManagement.Application.Features.SupportType.Queries;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class GetActiveSupportTypesQueryHandler : IRequestHandler<GetActiveSupportTypesQuery, List<SupportTypeDto>>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly IMapper _mapper;

        public GetActiveSupportTypesQueryHandler(ISupportTypeRepository supportTypeRepository, IMapper mapper)
        {
            _supportTypeRepository = supportTypeRepository ?? throw new ArgumentNullException(nameof(supportTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<SupportTypeDto>> Handle(GetActiveSupportTypesQuery request, CancellationToken cancellationToken)
        {
            var activeTypes = await _supportTypeRepository.GetActiveAsync(); // Assuming GetActiveAsync exists in the repository interface
            return _mapper.Map<List<SupportTypeDto>>(activeTypes);
        }
    }
} 
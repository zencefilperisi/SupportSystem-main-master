using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Commands;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using SupportRequestManagement.Domain.Entities;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class CreateSupportTypeCommandHandler : IRequestHandler<CreateSupportTypeCommand, SupportTypeDto>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly IMapper _mapper;

        public CreateSupportTypeCommandHandler(ISupportTypeRepository supportTypeRepository, IMapper mapper)
        {
            _supportTypeRepository = supportTypeRepository ?? throw new ArgumentNullException(nameof(supportTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SupportTypeDto> Handle(CreateSupportTypeCommand request, CancellationToken cancellationToken)
        {
            // Optional: Check if a type with the same name already exists
            // var existingType = await _supportTypeRepository.GetByNameAsync(request.Name); // Assuming GetByNameAsync exists
            // if (existingType != null)
            // {
            //     throw new Exception($"Support Type with name '{request.Name}' already exists."); // Use custom exception
            // }

            var supportType = _mapper.Map<Domain.Entities.SupportType>(request);
            supportType.CreatedAt = DateTime.UtcNow;

            await _supportTypeRepository.AddAsync(supportType);

            return _mapper.Map<SupportTypeDto>(supportType);
        }
    }
}

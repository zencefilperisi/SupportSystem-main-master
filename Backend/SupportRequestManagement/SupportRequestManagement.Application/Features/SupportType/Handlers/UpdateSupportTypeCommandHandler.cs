using AutoMapper;
using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Commands;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using SupportRequestManagement.Domain.Interfaces;
using System;
using System.Collections.Generic; // For KeyNotFoundException
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class UpdateSupportTypeCommandHandler : IRequestHandler<UpdateSupportTypeCommand, SupportTypeDto>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;
        private readonly IMapper _mapper;

        public UpdateSupportTypeCommandHandler(ISupportTypeRepository supportTypeRepository, IMapper mapper)
        {
            _supportTypeRepository = supportTypeRepository ?? throw new ArgumentNullException(nameof(supportTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SupportTypeDto> Handle(UpdateSupportTypeCommand request, CancellationToken cancellationToken)
        {
            var supportTypeToUpdate = await _supportTypeRepository.GetByIdAsync(request.Id);

            if (supportTypeToUpdate == null)
            {
                throw new KeyNotFoundException($"SupportType with ID '{request.Id}' not found."); // Replace with custom NotFoundException
            }

            // Optional: Check if another type with the new name already exists (excluding the current one)
            // var existingType = await _supportTypeRepository.GetByNameAsync(request.Name);
            // if (existingType != null && existingType.Id != request.Id)
            // {
            //     throw new Exception($"Another Support Type with name '{request.Name}' already exists."); // Use custom exception
            // }

            // Use AutoMapper to map updated fields from the command to the existing entity
            _mapper.Map(request, supportTypeToUpdate);

            await _supportTypeRepository.UpdateAsync(supportTypeToUpdate);

            // Return the updated DTO
            return _mapper.Map<SupportTypeDto>(supportTypeToUpdate);
        }
    }
}

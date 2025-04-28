using MediatR;
using SupportRequestManagement.Application.Contracts.Persistence;
using SupportRequestManagement.Application.Features.SupportType.Queries;
using SupportRequestManagement.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SupportRequestManagement.Application.Features.SupportType.Handlers
{
    public class GetAllSupportTypesQueryHandler : IRequestHandler<GetAllSupportTypesQuery, List<SupportTypeDto>>
    {
        private readonly ISupportTypeRepository _supportTypeRepository;

        public GetAllSupportTypesQueryHandler(ISupportTypeRepository supportTypeRepository)
        {
            _supportTypeRepository = supportTypeRepository;
        }

        public async Task<List<SupportTypeDto>> Handle(GetAllSupportTypesQuery request, CancellationToken cancellationToken)
        {
            var supportTypes = await _supportTypeRepository.GetAllAsync();

            var supportTypeDtos = supportTypes.Select(st => new SupportTypeDto
            {
                Id = st.Id,
                Name = st.Name,
                Description = st.Description
            }).ToList();

            return supportTypeDtos;
        }
    }
}

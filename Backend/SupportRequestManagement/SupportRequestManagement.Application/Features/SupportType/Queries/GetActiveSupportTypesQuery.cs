using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.SupportType.Queries
{
    public class GetActiveSupportTypesQuery : IRequest<List<SupportTypeDto>>
    {
        // No parameters needed for this query
    }
} 
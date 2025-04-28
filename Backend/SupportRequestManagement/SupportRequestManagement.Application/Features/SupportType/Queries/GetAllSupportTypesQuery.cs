using MediatR;
using SupportRequestManagement.Application.Features.SupportType.Dtos;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.SupportType.Queries
{
    public class GetAllSupportTypesQuery : IRequest<List<SupportTypeDto>>
    {
        // Optional: Add pagination parameters if needed
        // public int PageNumber { get; set; } = 1;
        // public int PageSize { get; set; } = 10;
    }
}

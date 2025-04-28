using MediatR;
using SupportRequestManagement.Application.Features.User.Dtos;
using System.Collections.Generic;

namespace SupportRequestManagement.Application.Features.User.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        // Şimdilik parametre almasına gerek yok. Tüm kullanıcıları getirir.
    }
}

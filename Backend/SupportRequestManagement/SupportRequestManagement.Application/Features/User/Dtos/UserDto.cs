using System;

namespace SupportRequestManagement.Application.Features.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }              // Kullanıcının benzersiz kimliği
        public string FirstName { get; set; }     // Adı
        public string LastName { get; set; }      // Soyadı
        public string Email { get; set; }         // E-posta adresi
        public string Role { get; set; }          // Rolü (Admin, User vs.)
        public DateTime CreatedAt { get; set; }   // Oluşturulma tarihi
    }
}


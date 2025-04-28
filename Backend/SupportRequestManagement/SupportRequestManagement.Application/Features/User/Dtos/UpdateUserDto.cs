using System;

namespace SupportRequestManagement.Application.Features.User.Dtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }              // Güncellenecek kullanıcının ID'si
        public string FirstName { get; set; }     // Adı
        public string LastName { get; set; }      // Soyadı
        public string Email { get; set; }         // E-posta adresi
        public string Role { get; set; }          // Rol (örneğin: "Admin", "User")
        public string? Password { get; set; }     // (Opsiyonel) Yeni şifre (değiştirilecekse)

        // Şifre güncellenmiyorsa boş geçilebilir
    }
}

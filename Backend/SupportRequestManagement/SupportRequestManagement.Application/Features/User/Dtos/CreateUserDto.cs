using System;

namespace SupportRequestManagement.Application.Features.User.Dtos
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }     // Kullanıcının adı
        public string LastName { get; set; }      // Soyadı
        public string Email { get; set; }         // E-posta adresi
        public string Password { get; set; }      // Şifre (hash'lenmiş olarak işlenmesi önerilir)
        public string Role { get; set; }          // Kullanıcı rolü (örneğin: "Admin", "User")

        // İstersen başka alanlar da ekleyebilirsin, örneğin telefon numarası, kullanıcı adı vb.
    }
}

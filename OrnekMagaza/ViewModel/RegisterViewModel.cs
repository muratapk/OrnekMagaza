using System.ComponentModel.DataAnnotations;

namespace OrnekMagaza.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad Soyad alanı en fazla 50 karakter olabilir.")]
        public string AdSoyad { get; set; } = string.Empty;
        [Required(ErrorMessage = "Kullanıcı Adı alanı zorunludur.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Parola alanı zorunludur.")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength = 6, ErrorMessage = "Parola en az 6, en fazla 20 karakter olabilir.")]
        public string Password { get; set; }= string.Empty;
        [Required(ErrorMessage = "Parola Doğrulama alanı zorunludur.")]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

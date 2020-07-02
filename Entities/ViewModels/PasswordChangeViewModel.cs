using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class PasswordChangeViewModel : IEntity
    {
        [Required(ErrorMessage = "Şifre kısmını boş geçmeyiniz!")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Şifreniz en az 8 en çok 16 karakter içerebilir")]
        [Display(Name = "Eski Şifre")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Şifre kısmını boş geçmeyiniz!")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Şifreniz en az 8 en çok 16 karakter içerebilir")]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare("NewPassword",ErrorMessage = "Şifre ve Şifre Tekrar uyuşmuyor.")]
        public string NewPasswordAgain { get; set; }
    }
}

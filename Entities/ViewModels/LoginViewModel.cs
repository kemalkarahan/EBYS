using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class LoginViewModel : IEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [Display(Name = "E-posta")]
        [StringLength(maximumLength: 128)]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "Şifre kısmını boş geçmeyiniz!")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Şifreniz en az 8 en çok 16 karakter içerebilir")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}

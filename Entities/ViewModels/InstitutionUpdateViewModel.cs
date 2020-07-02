using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class InstitutionUpdateViewModel : IEntity
    {
        [Display(Name = "Kuruluş İsmi")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [MaxLength(512)]
        [MinLength(2)]
        public string Name { get; set; }
        [Display(Name = "Kuruluş Domain")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [MaxLength(64)]
        [MinLength(6)]
        public string Domain { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 64, MinimumLength = 6, ErrorMessage = "Bu kısım en az 6 en çok 64 karakter içerebilir!")]
        [Display(Name = "Sunucu Smtp Host")]
        public string SmtpHost { get; set; }
        [Display(Name = "Sunucu Smtp Port")]
        public int SmtpPort { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 64, MinimumLength = 1, ErrorMessage = "Bu kısım en az 10 en çok 128 karakter içerebilir!")]
        [Display(Name = "Kurum Bilgiendirme E-Postası")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 32, ErrorMessage = "Bu kısım en çok 32 karakter içerebilir!")]
        [Display(Name = "E-Posta Şifre")]
        public string Password { get; set; }
        public IFormFile Logo { get; set; }
    }
}

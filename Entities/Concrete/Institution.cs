using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Institution : IEntity
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 512, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 512 karakter içerebilir!")]
        [Display(Name = "Kurum İsmi")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 64, MinimumLength = 6, ErrorMessage = "Bu kısım en az 6 en çok 64 karakter içerebilir!")]
        [Display(Name = "Kurum Domain")]
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
        [StringLength(maximumLength: 8, MinimumLength = 1)]
        public string Logo { get; set; }
    }
}

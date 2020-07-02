using Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Duty : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Staff")]
        [Display(Name = "Tanımlayan")]
        public int FromId { get; set; }
        public virtual Staff From { get; set; }
        [ForeignKey("Staff")]
        [Display(Name = "Muhattap")]
        public int To { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Bu kısım boş geçilemez!")]
        [StringLength(maximumLength:128, MinimumLength =2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Görev Başlığı")]
        public string TaskName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Görev Tanımı")]
        public string TaskType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 512, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 512 karakter içerebilir!")]
        [Display(Name = "Görev Açıklaması")]
        public string TaskDescription { get; set; }
        [Display(Name = "Başlama Tarih")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Bitiş Tarih")]
        public DateTime EndDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Önem Derecesi")]
        public string Category { get; set; }
        [Required]
        public string ReminderId { get; set; }
    }
}

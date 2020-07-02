using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class ChargeTaskViewModel : IEntity
    {
        public List<Staff> Staffs { get; set; }
        [Display(Name = "Muhattap")]
        public List<int> To { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Görev Başlığı")]
        public string TaskName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 512, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 512 karakter içerebilir!")]
        [Display(Name = "Görev Açıklaması")]
        public string TaskDescription { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Görev Tanımı")]
        public string TaskType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 128, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 128 karakter içerebilir!")]
        [Display(Name = "Önem Derecesi")]
        public string Category { get; set; }
        [Display(Name = "Zaman Aralığı")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

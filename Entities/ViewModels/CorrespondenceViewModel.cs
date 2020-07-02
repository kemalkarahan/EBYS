using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class CorrespondenceViewModel : IEntity
    {
        public List<Staff> Staffs { get; set; }
        public List<int> ToList { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 56, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 56 karakter içerebilir!")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 512, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 512 karakter içerebilir!")]
        [Display(Name = "İçerik")]
        public string Context { get; set; }
    }
}

using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class AddNewStaffViewModel : IEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 14, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 14 karakter içerebilir!")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 14, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 14 karakter içerebilir!")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 64, MinimumLength = 11, ErrorMessage = "Bu kısım en az 11 en çok 64 karakter içerebilir!")]
        [Display(Name = "Kullanıcı İsmi")]
        public string Nickname { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 64, MinimumLength = 3, ErrorMessage = "Bu kısım en az 3 en çok 64 karakter içerebilir!")]
        [Display(Name = "Ünvan")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Cinsiyet")]
        [StringLength(maximumLength: 5, MinimumLength = 2)]
        public string Gender { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Bu kısım boş geçilemez!")]
        [Display(Name = "Birim")]
        public int UnitId { get; set; }
        public List<int> OfficeList { get; set; }
        public List<int> ChairList { get; set; }
        public List<Staff> Staffs { get; set; }
        public List<Unit> Units { get; set; }
    }
}

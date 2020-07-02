using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class AddNewUnitViewModel : IEntity
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 64, MinimumLength = 5, ErrorMessage = "Bu kısım en az 5 en çok 64 karakter içerebilir!")]
        [Display(Name = "Birim İsmi")]
        public string Name { get; set; }
        [Display(Name = "Üst Birim")]
        public int UpperUnitId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 28, MinimumLength = 3, ErrorMessage = "Bu kısım en az 3 en çok 28 karakter içerebilir!")]
        [Display(Name = "Amir Ofis İsmi:")]
        public string Manager { get; set; }
        public List<Office> Offices { get; set; }
        public List<Unit> Units { get; set; }
    }
}

using Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Unit : IEntity
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez")]
        [StringLength(maximumLength: 64, MinimumLength = 5, ErrorMessage = "Bu kısım en az 5 en çok 64 karakter içerebilir!")]
        [Display(Name = "Birim İsmi")]
        public string Name { get; set; }
        [ForeignKey("UpperUnit")]
        [Display(Name = "Üst Birim")]
        public int UpperUnitId { get; set; }
        public virtual Unit UpperUnit { get; set; }
        public List<Office> Offices { get; set; }
    }
}

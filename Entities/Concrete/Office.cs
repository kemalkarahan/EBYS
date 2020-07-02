using Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Office : IEntity
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength:28, MinimumLength = 3, ErrorMessage = "Bu kısım en az 3 en çok 28 karakter içerebilir!")]
        [Display(Name = "Ofis İsmi")]
        public string Name { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsManager { get; set; }
        public List<Chair> Chairs { get; set; }
    }
}

using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class AboutMe : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Personel")]
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        [StringLength(maximumLength:512, ErrorMessage = "Bu kısım en çok 512 karakter içerebilir!")]
        [Display(Name = "Eğitim")]
        public string Education { get; set; }
        [StringLength(maximumLength: 512, ErrorMessage = "Bu kısım en çok 512 karakter içerebilir!")]
        [Display(Name = "Yetenekler")]
        public string Skills { get; set; }
        [StringLength(maximumLength: 512, ErrorMessage = "Bu kısım en çok 512 karakter içerebilir!")]
        [Display(Name = "Açıklama")]
        public string Notes { get; set; }

        public AboutMe()
        {
            Education = "Kullanıcı tarafından sisteme girilmiş bir eğitim bilgisi mevcut değil.";
            Skills = "Kullanıcı tarafından sisteme girilmiş bir yetenek bilgisi mevcut değil.";
            Notes = "Kullanıcı tarafından sisteme girilmiş bir açıklama bilgisi mevcut değil.";
        }
    }
}

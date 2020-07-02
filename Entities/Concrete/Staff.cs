using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Staff:IEntity
    {
        public int Id { get; set; }
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
        [StringLength(maximumLength: 5, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 5 karakter içerebilir!")]
        [Display(Name = "Cinsiyet")]
        public string Gender { get; set; }
        public string ProfileImage { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("Unit")]
        [Display(Name = "Birim")]
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre kısmını boş geçmeyiniz!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        public virtual List<Chair> Chairs { get; set; }
        public virtual List<Draft> Drafts { get; set; }
        public virtual List<Inbox> Inboxes { get; set; }
        public virtual List<Sent> Sents { get; set; }
        public virtual List<Trash> Trashes { get; set; }
    }
}

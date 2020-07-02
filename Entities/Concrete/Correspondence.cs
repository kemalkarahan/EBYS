using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Correspondence : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Staff")]
        public int FromId { get; set; }
        public virtual Staff From { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 56, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 56 karakter içerebilir!")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength: 512, MinimumLength = 2, ErrorMessage = "Bu kısım en az 2 en çok 512 karakter içerebilir!")]
        [Display(Name = "İçerik")]
        public string Context { get; set; }
        [Display(Name = "Gönderilme Tarihi")]
        public DateTime SentAt { get; set; }
        public List<To> Tos { get; set; }
        public List<Staff> To { get; set; }
        public virtual List<Inbox> Inboxes { get; set; }
        public virtual List<Sent> Sents { get; set; }
        public virtual List<Trash> Trashes { get; set; }
    }
}

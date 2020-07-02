using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Document : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Staff")]
        [Display(Name = "Evrak Sahibi")]
        public int OwnerId { get; set; }
        public virtual Staff Owner { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage = "Bu kısım boş geçilemez!")]
        [StringLength(maximumLength:256, MinimumLength =2, ErrorMessage = "Bu kısım en az 2 en çok 256 karakter içerebilir!")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Yüklenme Tarihi")]
        public DateTime CreatedAt { get; set; }
        public bool IsAddition { get; set; }
        public bool HasAdditions { get; set; }
        [Display(Name = "İlgili")]
        public virtual List<DocumentRelated> DocumentRelateds { get; set; }
        [Display(Name = "Ekler")]
        public virtual List<DocumentAttachment> DocumentAttachments { get; set; }
    }
}

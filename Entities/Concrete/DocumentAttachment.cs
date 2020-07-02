using Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class DocumentAttachment : IEntity
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        [ForeignKey("DocumentAttachment")]
        [Display(Name = "Ek Evrak No")]
        public int DocumentAttachmentId { get; set; }
        
    }
}

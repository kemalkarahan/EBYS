using Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class DocumentRelated : IEntity
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }        
        [ForeignKey("DocumentRelated")]
        [Display(Name = "İlgili Evrak No")]
        public int DocumentRelatedId { get; set; }
        
    }
}

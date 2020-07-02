using Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Submission : IEntity
    {
        public int Id { get; set; }
        [ForeignKey("Staff")]
        [Display(Name = "Gönderici")]
        public int From { get; set; }
        [ForeignKey("Staff")]
        [Display(Name = "Alıcı")]
        public int To { get; set; }
        [Display(Name = "Gönderilme Tarihi")]
        public DateTime SentAt { get; set; }
        [Display(Name = "Evrak")]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        [Display(Name = "Evrak Türü")]
        public string Category { get; set; }
        [Display(Name = "İvedilik Durumu")]
        public bool IsUrgent { get; set; }
    }
}

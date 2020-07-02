using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class IndexTaskInfo : IEntity
    {
        [Display(Name = "Görev Başlığı")]
        public string TaskName { get; set; }
        [Display(Name = "Görev Tanımı")]
        public string TaskType { get; set; }
        [Display(Name = "Önem Derecesi")]
        public string Category { get; set; }
        public string Datetime { get; set; }
    }
}

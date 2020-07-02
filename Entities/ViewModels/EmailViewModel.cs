using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class EmailViewModel : IEntity
    {
        public string From { get; set; }
        [Required(ErrorMessage = "Kime kısmı boş geçilemez")]
        [EmailAddress]
        public string To { get; set; }
        [Required(ErrorMessage = "Başlık kısmı boş geçilemez")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "İçerik kısmı boş geçilemez")]
        public string Textarea { get; set; }
        public List<IFormFile> Attachment { get; set; }
        public List<string> FilePaths { get; set; }
        public string Password { get; set; }
    }
}

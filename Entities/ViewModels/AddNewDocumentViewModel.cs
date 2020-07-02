using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class AddNewDocumentViewModel : IEntity
    {
        [Required(ErrorMessage = "Bir dosya eklemelisiniz.")]
        public IFormFile Document { get; set; }
        public List<string> AttachedExistedDocuments { get; set; }
        public List<string> RelatedExistedDocument { get; set; }
        public List<IFormFile> AttachedNewDocuments { get; set; }
        public List<IFormFile> RelatedNewDocuments { get; set; }
    }
}

using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class ShareDocumentViewModel : IEntity
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public List<string> SendingList { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}

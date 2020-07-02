using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class HomeIndexViewModel : IEntity
    {
        public List<Document> Documents { get; set; }
        public List<IndexTaskInfo> IndexTaskInfos { get; set; }
    }
}

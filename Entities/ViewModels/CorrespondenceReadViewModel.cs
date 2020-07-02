using Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class CorrespondenceReadViewModel : IEntity
    {
        public List<Correspondence> Correspondences { get; set; }
        public Correspondence Correspondence { get; set; }
    }
}

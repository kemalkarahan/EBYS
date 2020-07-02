using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Draft : IEntity
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        [StringLength(maximumLength: 56)]
        public string Title { get; set; }
        [StringLength(maximumLength: 512)]
        public string Context { get; set; }
    }
}

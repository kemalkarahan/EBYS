using Entities.Abstract;

namespace Entities.Concrete
{
    public class Trash : IEntity
    {
        public int Id { get; set; }
        public int CorrespondenceId { get; set; }
        public int StaffId { get; set; }
    }
}

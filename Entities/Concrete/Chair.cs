using Entities.Abstract;

namespace Entities.Concrete
{
    public class Chair : IEntity
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public int StaffId { get; set; }
    }
}

using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class SubmissionDal : EfEntityRepositoryBase<Submission, EfContext>, ISubmissionDal
    {
        public SubmissionDal(EfContext efContext) : base(efContext)
        {

        }
    }
}

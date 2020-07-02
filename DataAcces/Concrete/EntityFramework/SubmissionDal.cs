using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class SubmissionDal : EfEntityRepositoryBase<Submission, EfContext>, ISubmissionDal
    {
        public SubmissionDal(EfContext efContext) : base(efContext)
        {

        }
    }
}

using Entities.Concrete;
using Entities.ViewModels;
using System.Threading.Tasks;

namespace Services.NativeServices.Abstract
{
    public interface IMessageService
    {
        Task Default(EmailViewModel email);
        Task PasswordReset(Staff staff);
        Task<string> NewStaffAdded(string nickname, string name, string lastName);
    }
}

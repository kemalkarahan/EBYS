using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.NativeServices.Abstract
{
    public interface ISystemService
    {
        Task UploadImage(IFormFile image, string folder, string imageName);
        Task<bool> LoginControl(LoginViewModel login);
        void CreateFolder(string folderName);
        void DeleteFolder(string folderName);
        void ImageResizing(string fileName, string folder);
    }
}

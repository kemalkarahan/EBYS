using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.NativeServices.Abstract;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Abstract;

namespace Services.NativeServices.Concrete
{
    public class SystemSevice : ISystemService
    {
        private readonly IStaffManager staffManager;
        private readonly IUtility utilities;
        private readonly IWebHostEnvironment hostEnvironment;

        public SystemSevice(IStaffManager staffManager,
                            IUtility utilities,
                            IWebHostEnvironment hostEnvironment)
        {
            this.staffManager = staffManager;
            this.utilities = utilities;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<bool> LoginControl(LoginViewModel login)
        {
            Staff staff = await staffManager.RetrieveAsync(login.Nickname);
            string convertedPassword = utilities.ConvertToHashCode(login.Password);
            if (staff != null && staff.Password == convertedPassword)
            {
                return true;
            }
            return false;
        }

        public void CreateFolder(string folderName)
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, @"files\user\documents");

            string fullPath = Path.Combine(directory, folderName);
            Directory.CreateDirectory(fullPath);
        }

        public void DeleteFolder(string folderName)
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, @"files\user\documents");
            string image = Path.Combine(hostEnvironment.WebRootPath, @"files\user\images");

            if (Directory.Exists(Path.Combine(directory, folderName)))
                Directory.Delete(Path.Combine(directory, folderName));
            if (File.Exists(Path.Combine(image, folderName + ".jpg")))
                File.Delete(Path.Combine(image, folderName + ".jpg"));
        }

        public async Task UploadImage(IFormFile image, string folder, string imageName)
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, @"files\" + folder + @"\images\original\");

            string fullPath = Path.Combine(directory, imageName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        //Reference for ImageResizing() :
        //Code Example: Asp Mvc Core Photo Upload and Resize
        //Youtube: https://www.youtube.com/watch?v=h1kqeBpexS4
        //Website: https://codedocu.com/Details?d=2210&a=9&f=365&l=0
        public void ImageResizing(string fileName, string folder)
        {
            //---------------< Image_resize() >---------------

            const long quality = 75L;
            const int newScale = 300;

            string originalDirectory = Path.Combine(hostEnvironment.WebRootPath, @"files\" + folder + @"\images\original\");
            string outputImageDirectory = Path.Combine(hostEnvironment.WebRootPath, @"files\" + folder + @"\images\");
            string originalImagePath = Path.Combine(originalDirectory, fileName);
            string outputImagePath = Path.Combine(outputImageDirectory, fileName);

            Bitmap sourceBitmap = new Bitmap(originalImagePath);

            //< create Empty Drawarea >
            var newDrawArea = new Bitmap(newScale, newScale);
            //</ create Empty Drawarea >

            using (var graphicDrawArea = Graphics.FromImage(newDrawArea))
            {
                //< setup >
                graphicDrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphicDrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphicDrawArea.CompositingMode = CompositingMode.SourceCopy;

                graphicDrawArea.DrawImage(sourceBitmap, 0, 0, newScale, newScale);
                //</ setup >

                using (var output = File.Open(outputImagePath, FileMode.Create))

                {

                    //< setup jpg >

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >

                    //< save Bitmap as Jpg >

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    newDrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose();

                    output.Close();

                    //</ save Bitmap as Jpg >

                }
                //--</ Output as .Jpg >--

                graphicDrawArea.Dispose();
            }
            sourceBitmap.Dispose();

            //---------------</ Image_resize() >---------------
            File.Delete(originalImagePath);
        }
    }
}

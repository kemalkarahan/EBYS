using System.Threading.Tasks;

namespace Services.Adapter.Abstract
{
    public interface IFileCompressionService
    {
        Task<byte[]> Compression(int fileId, string zipFileName, string fileDirectoryName);
    }
}

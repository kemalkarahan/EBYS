using System.Security.Cryptography;
using System.Text;
using Utilities.Abstract;

namespace Utilities.Concrete
{
    public class Utility : IUtility
    {
        public string ConvertToHashCode(string toHashCode)
        {
            using SHA512 sha512Hash = SHA512.Create();
            var plainBytes = Encoding.UTF8.GetBytes(toHashCode + "sha512");
            byte[] bytes = sha512Hash.ComputeHash(plainBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

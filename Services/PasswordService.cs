using System.Security.Cryptography;
using System.Text;
namespace Products_Crud.Services
{
    public class PasswordService
    {
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}

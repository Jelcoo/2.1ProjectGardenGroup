using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public class PasswordTools
    {
        public static string HashPassword(string salt, string password)
        {
            string salty = salt + password;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(salty));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string GenerateVerifyCode(int codeLength)
        {
            StringBuilder codeBuilder = new StringBuilder();
            for (int i = 0; i < codeLength; i++)
            {
                codeBuilder.Append(GenericTools.GenerateNumber(0, 9));
            }

            return codeBuilder.ToString();
        }
    }
}
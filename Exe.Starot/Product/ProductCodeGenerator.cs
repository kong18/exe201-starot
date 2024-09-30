using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Product
{
    public class ProductCodeGenerator
    {
        public static string GenerateProductCode(string prefix = "PROD")
        {
            // Get the current date in yyyyMMdd format
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");

            // Generate a random 6-character alphanumeric string
            string randomPart = GetRandomString(6);

            // Combine prefix, date, and random string to create the product code
            return $"{prefix}-{datePart}-{randomPart}";
        }

        private static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[4];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(buffer);
                    int num = BitConverter.ToInt32(buffer, 0);
                    result.Append(chars[Math.Abs(num % chars.Length)]);
                }
            }

            return result.ToString();
        }
    }
}

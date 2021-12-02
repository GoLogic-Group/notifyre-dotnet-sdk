using System;
using System.Security.Cryptography;
using System.Text;

namespace Notifyre.Infrastructure.Functions
{
    public static class GenerateSignature
    {
        public static string Execute(string message, string key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] messageBytes = encoding.GetBytes(message);
            byte[] keyBytes = encoding.GetBytes(key);
            byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
            {
                hashBytes = hash.ComputeHash(messageBytes);
            }

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

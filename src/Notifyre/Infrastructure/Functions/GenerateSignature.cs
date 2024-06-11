using System;
using System.Security.Cryptography;
using System.Text;

namespace Notifyre.Infrastructure.Functions
{
    public static class GenerateSignature
    {
        public static string Execute(string message, string key)
        {
            key = key ?? "";

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] hashBytes;

            using (var hash = new HMACSHA256(keyBytes))
            {
                hashBytes = hash.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

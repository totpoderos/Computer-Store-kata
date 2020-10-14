using System;
using System.Security.Cryptography;
using System.Text;

namespace ComputerStore
{
    public class EncryptionService
    {
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPassword = SHA256.Create().ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
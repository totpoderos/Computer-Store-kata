using System;
using System.Security.Cryptography;
using System.Text;

namespace ComputerStore.Services
{
    public static class EncryptionService
    {
        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedPassword = SHA256.Create().ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
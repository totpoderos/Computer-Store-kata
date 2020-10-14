using System;
using ComputerStore.Database;
using ComputerStore.Domain;

namespace ComputerStore.Services
{
    public static class AuthenticationService
    {
        public static void AuthenticateRoot(string username, string password)
        {
            User user = QueryService.FinsUserByUsername(username);
            if (user == null) 
                throw new Exception("Authentication failed. User does not exist. Username: " + username);
            string encryptedPassword = EncryptionService.HashPassword(password);
            if (!user.Password.Equals(encryptedPassword)) 
                throw new Exception($"Authentication failed. Wrong credentials. Username: {username} Password: {password}");
        }
    }
}
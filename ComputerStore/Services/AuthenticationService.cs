using System;
using ComputerStore.Database;
using ComputerStore.Domain;

namespace ComputerStore.Services
{
    public static class AuthenticationService
    {
        public static void AuthenticateRoot(string username, string password)
        {
            User user = QueryService.FindUserByUsername(username);
            if (user == null) 
                throw new Exception("Authentication failed. User does not exist. Username: " + username);
            string encryptedPassword = EncryptionService.HashPassword(password);
            if (!user.Password.Equals(encryptedPassword)) 
                throw new Exception($"Authentication failed. Wrong credentials. Username: {username} Password: {password}");
            if (!user.IsRoot)
                throw new Exception($"Authentication failed. User is not root");
        }

        public static User AuthenticateUser(string username, string password)
        {
            User user = QueryService.FindUserByUsername(username);
            if (user == null) 
                throw new Exception("Authentication failed. User does not exist. Username: " + username);
            string encryptedPassword = EncryptionService.HashPassword(password);
            if (!user.Password.Equals(encryptedPassword)) 
                throw new Exception($"Authentication failed. Wrong credentials. Username: {username} Password: {password}");
            return user;
        }
    }
}
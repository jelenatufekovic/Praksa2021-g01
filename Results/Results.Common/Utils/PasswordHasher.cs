using System;
using System.Security.Cryptography;

namespace Results.Common.Utils
{
    public class PasswordHasher
    {
        public static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[24];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

        public static string HashUsingPbkdf2(string password, byte[] salt, int iterations)
        {
            var bytes = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);

            //return Convert.ToBase64String(salt) + "|" + iterations + "|" + Convert.ToBase64String(bytes.GetBytes(24));
            return Convert.ToBase64String(bytes.GetBytes(64));
        }
    }
}

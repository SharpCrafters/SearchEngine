using System;
using System.Security.Cryptography;
using System.Text;

namespace SearchEngine
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;

        private const int HashSize = 20;

        private const int IterationCount = 10000;

        public static string HashPassword(string Password)
        {
            // Генерация соли
            byte[] Salt;
            new RNGCryptoServiceProvider().GetBytes(Salt = new byte[SaltSize]);

            // Создание хеша
            var pbkdf2 = new Rfc2898DeriveBytes(Password, Salt, IterationCount);
            var Hash = pbkdf2.GetBytes(HashSize);

            // Комбинирование соли и хеша
            var HashBytes = new byte[SaltSize + HashSize];
            Array.Copy(Salt, 0, HashBytes, 0, SaltSize);
            Array.Copy(Hash, 0, HashBytes, SaltSize, HashSize);

            // Конвертация в base64
            var Base64Hash = Convert.ToBase64String(HashBytes);

            return Base64Hash;
        }

        public static bool VerifyPassword(string Password, string HashedPassword)
        {
            // Конвертация из base64
            var HashBytes = Convert.FromBase64String(HashedPassword);

            // Извлечение соли
            var Salt = new byte[SaltSize];
            Array.Copy(HashBytes, 0, Salt, 0, SaltSize);

            // Создание хеша введенного пароля
            var pbkdf2 = new Rfc2898DeriveBytes(Password, Salt, IterationCount);
            var Hash = pbkdf2.GetBytes(HashSize);

            // Сравнение хешей
            for (var i = 0; i < HashSize; i++)
            {
                if (HashBytes[i + SaltSize] != Hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

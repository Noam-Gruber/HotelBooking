using System.IO;
using System.Security.Cryptography;

namespace Common
{
    public static class Encription
    {
        /// <summary>
        /// Encrypts a plain text string using AES encryption.
        /// </summary>
        /// <param name="plain">The plain text to encrypt.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        public static byte[] EncryptAes(string plain, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key; aes.IV = iv;
            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plain);
            sw.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Decrypts AES-encrypted data to a plain text string.
        /// </summary>
        /// <param name="data">The encrypted data.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The decrypted plain text string.</returns>
        public static string DecryptAes(byte[] data, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key; aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(data);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}

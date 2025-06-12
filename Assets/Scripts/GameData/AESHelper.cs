using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AESHelper
{
    private static readonly string encryptionKey = "asaqeio129w8n1nd92dnqcnkqwe"; // Replace with a secure key

    public static string Encrypt(string plainText)
    {
        byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
        byte[] iv = new byte[16]; // AES block size = 16 bytes (empty IV for simplicity)

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

            return Convert.ToBase64String(encrypted);
        }
    }

    public static string Decrypt(string cipherText)
    {
        byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
        byte[] iv = new byte[16];

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(cipherText), 0, Convert.FromBase64String(cipherText).Length);

            return Encoding.UTF8.GetString(decrypted);
        }
    }
}

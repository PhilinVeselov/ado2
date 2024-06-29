using System;
using System.Security.Cryptography;
using System.Text;
using kt8.Models;
using Microsoft.AspNetCore.Mvc;

namespace kt8.Controllers
{
    public class EncryptionController : Controller
    {
        public IActionResult Index()
        {
            return View(new EncryptionViewModel());
        }

        [HttpPost]
        public IActionResult Encrypt(EncryptionViewModel model)
        {
            if (string.IsNullOrEmpty(model.Algorithm) || string.IsNullOrEmpty(model.PlainText))
            {
                return View("Index", model);
            }

            switch (model.Algorithm.ToLower())
            {
                case "aes":
                    model.Key = GenerateAesKey();
                    model.EncryptedText = EncryptAes(model.PlainText, model.Key);
                    break;
                case "rsa":
                    RSAParameters rsaPublicKey;
                    RSAParameters rsaPrivateKey;
                    model.EncryptedText = EncryptRsa(model.PlainText, out rsaPublicKey, out rsaPrivateKey);
                    model.PublicKey = Convert.ToBase64String(rsaPublicKey.Modulus);
                    model.PrivateKey = Convert.ToBase64String(rsaPrivateKey.D);
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Unknown algorithm");
                    return View("Index", model);
            }

            return View("Result", model);
        }

        [HttpPost]
        public IActionResult Decrypt(EncryptionViewModel model)
        {
            if (string.IsNullOrEmpty(model.Algorithm) || string.IsNullOrEmpty(model.EncryptedText) || string.IsNullOrEmpty(model.Key))
            {
                return View("Result", model);
            }

            switch (model.Algorithm.ToLower())
            {
                case "aes":
                    model.DecryptedText = DecryptAes(model.EncryptedText, model.Key);
                    break;
                case "rsa":
                    RSAParameters rsaKeys = new RSAParameters
                    {
                        Modulus = Convert.FromBase64String(model.PublicKey),
                        D = Convert.FromBase64String(model.PrivateKey)
                    };
                    model.DecryptedText = DecryptRsa(model.EncryptedText, rsaKeys);
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Unknown algorithm");
                    return View("Result", model);
            }

            return View("Result", model);
        }

        private static string EncryptAes(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.GenerateIV();
                var iv = aes.IV;
                var encryptor = aes.CreateEncryptor(aes.Key, iv);

                using (var ms = new System.IO.MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static string DecryptAes(string encryptedText, string key)
        {
            var fullCipher = Convert.FromBase64String(encryptedText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                var iv = new byte[aes.BlockSize / 8];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Array.Copy(fullCipher, iv, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                var decryptor = aes.CreateDecryptor(aes.Key, iv);

                using (var ms = new System.IO.MemoryStream(cipher))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new System.IO.StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static string EncryptRsa(string plainText, out RSAParameters publicKey, out RSAParameters privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
                var data = Encoding.UTF8.GetBytes(plainText);
                var cipher = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(cipher);
            }
        }

        private static string DecryptRsa(string encryptedText, RSAParameters rsaKeys)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaKeys);
                var data = Convert.FromBase64String(encryptedText);
                var plainText = rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(plainText);
            }
        }

        private static string GenerateAesKey()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                return Convert.ToBase64String(aes.Key);
            }
        }
    }
}

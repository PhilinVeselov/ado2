using Microsoft.AspNetCore.Mvc;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Sodium;
using kt11.Models;
using Org.BouncyCastle.Crypto.Modes;

namespace kt11.Controllers
{
    public class EncryptionController : Controller
    {
        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 bytes key for AES
        private static readonly byte[] AesIv = Encoding.UTF8.GetBytes("abcdef9876543210"); // 16 bytes IV for AES

        public IActionResult Index()
        {
            return View(new EncryptionModel());
        }

        [HttpPost]
        public IActionResult AesEncrypt(EncryptionModel model)
        {
            model.EncryptedText = AesEncrypt(model.PlainText);
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult AesDecrypt(EncryptionModel model)
        {
            model.DecryptedText = AesDecrypt(model.EncryptedText);
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult LibsodiumEncrypt(EncryptionModel model)
        {
            model.EncryptedText = LibsodiumEncrypt(model.PlainText);
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult LibsodiumDecrypt(EncryptionModel model)
        {
            model.DecryptedText = LibsodiumDecrypt(model.EncryptedText);
            return View("Index", model);
        }

        private string AesEncrypt(string plainText)
        {
            var engine = new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            engine.Init(true, new ParametersWithIV(new KeyParameter(AesKey), AesIv));
            var inputBytes = Encoding.UTF8.GetBytes(plainText);
            var outputBytes = new byte[engine.GetOutputSize(inputBytes.Length)];
            var length = engine.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
            engine.DoFinal(outputBytes, length);
            return Convert.ToBase64String(outputBytes);
        }

        private string AesDecrypt(string encryptedText)
        {
            var engine = new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()));
            engine.Init(false, new ParametersWithIV(new KeyParameter(AesKey), AesIv));
            var inputBytes = Convert.FromBase64String(encryptedText);
            var outputBytes = new byte[engine.GetOutputSize(inputBytes.Length)];
            var length = engine.ProcessBytes(inputBytes, 0, inputBytes.Length, outputBytes, 0);
            engine.DoFinal(outputBytes, length);
            return Encoding.UTF8.GetString(outputBytes).TrimEnd('\0');
        }

        private string LibsodiumEncrypt(string plainText)
        {
            var key = SecretBox.GenerateKey();
            var nonce = SecretBox.GenerateNonce();
            var encryptedBytes = SecretBox.Create(Encoding.UTF8.GetBytes(plainText), nonce, key);
            return $"{Convert.ToBase64String(nonce)}:{Convert.ToBase64String(encryptedBytes)}";
        }

        private string LibsodiumDecrypt(string encryptedText)
        {
            var parts = encryptedText.Split(':');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid encrypted text format");
            }

            var nonce = Convert.FromBase64String(parts[0]);
            var encryptedBytes = Convert.FromBase64String(parts[1]);
            var key = SecretBox.GenerateKey(); 
            var decryptedBytes = SecretBox.Open(encryptedBytes, nonce, key);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

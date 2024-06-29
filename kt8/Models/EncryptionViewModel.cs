namespace kt8.Models
{
    public class EncryptionViewModel
    {
        public string PlainText { get; set; }
        public string EncryptedText { get; set; }
        public string DecryptedText { get; set; }
        public string Key { get; set; }
        public string Algorithm { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}

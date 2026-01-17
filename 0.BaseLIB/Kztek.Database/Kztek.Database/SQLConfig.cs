using Kztek.Tool;

namespace Kztek.Database
{
    public class SQLConfig
    {
        public string ServerName { get; set; } = string.Empty;
        public string Authentication { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;

        public static string GetEncodePassword(string password)
        {
            return CryptorEngine.Encrypt(password, true);
        }
        public static string GetDecodePassword(string encodedPassword)
        {
            return CryptorEngine.Decrypt(encodedPassword, true);
        }
    }
}

namespace Kztek.Object
{
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public object? OptionalData { get; set; }

        public AuthRequest(string username, string password)
        {
            OptionalData = null;
            Username = username;
            Password = password;
        }
    }
}

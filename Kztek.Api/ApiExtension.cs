namespace Kztek.Api
{
    public static class ApiExtension
    {
        public static string StandardlizeServerName(this string serverUrl)
        {
            if (serverUrl[^1] != '/' && serverUrl[^1] != '\\')
            {
                serverUrl += "/";
            }
            return serverUrl;
        }
    }
}

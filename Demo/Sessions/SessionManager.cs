using System.Text.Json;

namespace Demo.Sessions
{
    public class SessionManager
    {
        public static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

        public static void Set(string key, object value)
        {
            HttpContext.Session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(string key)
        {
            var value = HttpContext.Session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

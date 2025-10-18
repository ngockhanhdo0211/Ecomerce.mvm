// File này giúp ta gọi HttpContext.Session.Set("GioHang",ds) và Get<List<GioHangItem>>("GioHang")
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace ECommerceMVC.Helpers
{
    public static class SessionExtenstions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);

        }    
    }
    
}
using System.Net.Http;

namespace Rigid
{
    public static class Rigid
    {
        public static Request Get(string requestUri)
        {
            return new Request(requestUri, HttpMethod.Get);
        }
    }
}

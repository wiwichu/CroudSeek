using System.Net.Http;

namespace CroudSeek.Client.Services
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

    }
}

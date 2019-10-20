using DeepAI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceApps.Interfaces;
using SpaceApps.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpaceApps.Service
{
    public class StyleTransferService : IStyleTransferService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public StyleTransferService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public async Task<string> GetRandomNASAImageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://images-api.nasa.gov/search?q=Moon");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<RootObject>(await response.Content.ReadAsStringAsync()).Collection.Items;
                var randomImage = data[new Random().Next(0, data.Count)].Links[0].Href; // It gets the preview image

                return randomImage;
            }

            return string.Empty;
        }

        public string TransferFromUrl(string contentUrl, string styleUrl)
        {
            DeepAI_API api = new DeepAI_API(_configuration.GetValue<string>("DeepAISettings:ApiKey"));

            StandardApiResponse resp = api.callStandardApi("fast-style-transfer", new
            {
                content = contentUrl,
                style = styleUrl
            });

            return api.objectAsJsonString(resp);
        }

        public string TransferFromFile(string contentPath, string stylePath)
        {
            DeepAI_API api = new DeepAI_API(_configuration.GetValue<string>("DeepAISettings:ApiKey"));

            using var contentStream = File.OpenRead(contentPath);
            using var styleStream = File.OpenRead(stylePath);

            StandardApiResponse resp = api.callStandardApi("fast-style-transfer", new
            {
                content = contentStream,
                style = styleStream
            });

            return api.objectAsJsonString(resp);
        }
    }
}

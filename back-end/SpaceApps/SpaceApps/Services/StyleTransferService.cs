using DeepAI;
using Microsoft.Extensions.Configuration;
using SpaceApps.Interfaces;
using System.IO;

namespace SpaceApps.Service
{
    public class StyleTransferService : IStyleTransferService
    {
        private readonly IConfiguration _configuration;

        public StyleTransferService(IConfiguration configuration)
        {
            _configuration = configuration;
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

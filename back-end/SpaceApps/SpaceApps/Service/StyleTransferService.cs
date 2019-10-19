using DeepAI;
using Microsoft.Extensions.Configuration;
using SpaceApps.Interfaces;

namespace SpaceApps.Service
{
    public class StyleTransferService : IStyleTransferService
    {
        private readonly IConfiguration _configuration;

        public StyleTransferService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Transfer(string contentUrl, string styleUrl)
        {
            DeepAI_API api = new DeepAI_API(_configuration.GetValue<string>("DeepAISettings:ApiKey"));

            StandardApiResponse resp = api.callStandardApi("fast-style-transfer", new
            {
                content = contentUrl,
                style = styleUrl
            });

            return api.objectAsJsonString(resp);
        }
    }
}

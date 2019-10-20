using System.Threading.Tasks;

namespace SpaceApps.Interfaces
{
    public interface IStyleTransferService
    {
        Task<string> GetRandomNASAImageAsync();
        string TransferFromUrl(string contentUrl, string styleUrl);
        string TransferFromFile(string contentPath, string stylePath);
    }
}

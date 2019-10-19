using System.IO;

namespace SpaceApps.Interfaces
{
    public interface IStyleTransferService
    {
        string TransferFromUrl(string contentUrl, string styleUrl);
        string TransferFromFile(string contentPath, string stylePath);
    }
}

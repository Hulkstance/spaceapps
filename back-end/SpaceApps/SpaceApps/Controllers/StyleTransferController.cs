using Microsoft.AspNetCore.Mvc;
using SpaceApps.Interfaces;
using SpaceApps.Models;
using System.IO;
using System.Net.Http.Headers;

namespace SpaceApps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleTransferController : ControllerBase
    {
        private readonly IStyleTransferService _styleTransferService;

        public StyleTransferController(IStyleTransferService styleTransferService)
        {
            _styleTransferService = styleTransferService;
        }

        [HttpPost("urltransfer")]
        public string TransferFromUrl(StyleTransferFromUrl model)
        {
            return _styleTransferService.TransferFromUrl(model.ContentUrl, model.StyleUrl);
        }

        [HttpPost("filetransfer"), DisableRequestSizeLimit]
        public string TransferFromFile()
        {
            var files = Request.Form.Files;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Resources", "Images"));

            if (files[0].Length > 0 && files[1].Length > 0)
            {
                var contentPath = Path.Combine(pathToSave, ContentDispositionHeaderValue.Parse(files[0].ContentDisposition).FileName.Trim('"'));
                var stylePath = Path.Combine(pathToSave, ContentDispositionHeaderValue.Parse(files[1].ContentDisposition).FileName.Trim('"'));
                
                using (var stream = new FileStream(contentPath, FileMode.Create))
                {
                    files[0].CopyTo(stream);
                }

                using (var stream = new FileStream(stylePath, FileMode.Create))
                {
                    files[1].CopyTo(stream);
                }

                var result = _styleTransferService.TransferFromFile(contentPath, stylePath);

                System.IO.File.Delete(contentPath);
                System.IO.File.Delete(stylePath);

                return result;
            }

            return string.Empty;
        }
    }
}

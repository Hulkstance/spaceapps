using Microsoft.AspNetCore.Mvc;
using SpaceApps.Interfaces;
using SpaceApps.Models;

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

        // POST: api/StyleTransfer
        [HttpPost]
        public string Transfer(StyleTransfer model)
        {
            return _styleTransferService.Transfer(model.ContentUrl, model.StyleUrl);
        }
    }
}

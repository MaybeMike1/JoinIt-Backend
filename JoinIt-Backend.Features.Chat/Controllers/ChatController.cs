using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Chat.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {

        [HttpGet, ActionName("GetMessages")]
        public async Task<IActionResult> GetMessages()
        {
            return Ok();
        }

        [HttpPost, ActionName("SendMessage")]
        public async Task<IActionResult> SendMessage()
        {
            return Ok();
        }

    }
}

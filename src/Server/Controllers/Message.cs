using Blazor5Email.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor5Email.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Message : Controller
    {
        private IEmailService _emailService;

        public Message(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task Index()
        {
            await _emailService.SendRazorAsync("someone@contoso.com", "test message from blazor", "TestEmail");
        }

    }
}

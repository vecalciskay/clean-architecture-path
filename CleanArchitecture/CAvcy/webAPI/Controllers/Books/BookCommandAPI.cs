using CA_Services.Persistence;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;
using Framework;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers.Books
{
    [Route("api/books")]
    public class BookReaderCommandAPI : Controller
    {
        private readonly IApplicationService _applicationService;
        private static readonly ILogger _log = Serilog.Log.ForContext<BookReaderCommandAPI>();

        public BookReaderCommandAPI(IApplicationService applicationService)
            => _applicationService = applicationService;

        // POST api/<controller>
        [Route("create")]
        [HttpPost]
        public Task<IActionResult> Post([FromBody]Infrastructure.Commands.V1.Books.CreateBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, _log);

        // POST api/<controller>
        [Route("lend")]
        [HttpPost]
        public Task<IActionResult> Post([FromBody]Infrastructure.Commands.V1.Books.LendBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, _log);

        [Route("return")]
        [HttpPost]
        public Task<IActionResult> Post([FromBody]Infrastructure.Commands.V1.Books.ReturnBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, _log);
    }
}

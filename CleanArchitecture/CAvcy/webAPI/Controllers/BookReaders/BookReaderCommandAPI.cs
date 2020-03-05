using CA_Services.Persistence;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;
using Framework;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers.BookReaders
{
    [Route("api/readers")]
    public class BookReaderCommandAPI : Controller
    {
        private readonly IApplicationService _applicationService;
        private static readonly ILogger _log = Serilog.Log.ForContext<BookReaderCommandAPI>();

        public BookReaderCommandAPI(IApplicationService applicationService)
            => _applicationService = applicationService;

        // POST api/<controller>
        [Route("create")]
        [HttpPost]
        public Task<IActionResult> Post([FromBody]Infrastructure.Commands.V1.BookReaders.CreateReader request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle, _log);

    }
}

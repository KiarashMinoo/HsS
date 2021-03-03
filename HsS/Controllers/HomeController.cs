using HsS.Models.Entities;
using HsS.Services.Commands;
using HsS.Services.Commands.Handlers;
using HsS.Services.Queries;
using HsS.Services.Queries.Handlers;
using HsS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace HsS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueueRequestCommandHandler handler;
        private readonly IListSharesQueryHandler queryHandler;

        public HomeController(ILogger<HomeController> logger, IQueueRequestCommandHandler requestHandler, IListSharesQueryHandler queryHandler)
        {
            _logger = logger;
            this.handler = requestHandler;
            this.queryHandler = queryHandler;
        }

        public IActionResult Index()
        {
            ViewData["Shares"] = queryHandler.Handle(new ListSharesQuery());
            return View();
        }

        [HttpPost]
        public IActionResult Index(SaleShareViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var command = new QueueRequestCommand
                {
                    Request = new ShareOrder()
                    {
                        ShareId = viewModel.ShareId,
                        Quantity = viewModel.Quantity,
                        Type = Models.Enums.ShareOrderType.Sale,
                        UnitAmount = viewModel.UnitAmount
                    },
                    HubId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                handler.Handle(command);
            }

            return RedirectToAction();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

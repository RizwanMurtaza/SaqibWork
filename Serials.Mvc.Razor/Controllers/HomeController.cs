using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serials.Core;
using Serials.Services;

namespace Serials.Mvc.Razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISerialsAccessService _serialsAccessService;

        public HomeController(ISerialsAccessService serialsAccessService)
        {
            _serialsAccessService = serialsAccessService;
        }

        // GET: ReadersController
        public async Task<ActionResult> Index(string userName = "")
        {
            var readers = await
                _serialsAccessService.Find(new SearchRequest { SerialNumber = userName });
            return View(readers);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var model = new SerialViewModel();
            model.SerialNumber = _serialsAccessService.GenerateNewSerial().ConfigureAwait(true).GetAwaiter().GetResult();
            return View("~/Views/Home/CreateOrUpdate.cshtml", model);
        }

        // POST: ReadersController/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SerialViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Home/CreateOrUpdate.cshtml", model);

                await _serialsAccessService.Add(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Home/CreateOrUpdate.cshtml", model);
            }
        }

        [HttpGet]
        [Route("Edit/{serialNumber}")]
        public async Task<ActionResult> Edit(string serialNumber)
        {
            var serial = await _serialsAccessService.Single(serialNumber);
            serial.InputType = InputType.Update;

            ViewBag.ReaderId = serialNumber;

            return View("~/Views/Home/CreateOrUpdate.cshtml", serial);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [Route("Edit/{serialNumber}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SerialViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Home/CreateOrUpdate.cshtml", model);

                await _serialsAccessService.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("~/Views/Home/CreateOrUpdate.cshtml", model);
            }
        }

        [HttpGet]
        [Route("Delete/{serialNumber}")]
        public async Task<ActionResult> Delete(string serialNumber)
        {
            await _serialsAccessService.Delete(serialNumber);

            return RedirectToAction(nameof(Index));
        }
    }
}

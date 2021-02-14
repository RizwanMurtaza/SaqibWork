using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serials.Mvc.Razor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        [Route("Edit/{readerId}")]
        public async Task<ActionResult> Edit(string readerId)
        {
            var serial = await _serialsAccessService.Single(readerId);

            ViewBag.ReaderId = readerId;

            return View("~/Views/Home/CreateOrUpdate.cshtml", serial);
        }

        // POST: ReadersController/Edit/5
        [HttpPost]
        [Route("Edit/{readerId}")]
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
        [Route("Delete/{readerId}")]
        public async Task<ActionResult> Delete(Guid readerId)
        {
            // await _serialsAccessService.(readerId);

            return RedirectToAction(nameof(Index));
        }
    }
}

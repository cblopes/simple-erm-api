using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.MVC.Models;
using SimpleERP.MVC.Services;

namespace SimpleERP.MVC.Controllers
{
    public class ClientController : MainController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: ClientController
        public async Task<IActionResult> Index()
        {
            var clientsList = await _clientService.GetClients();

            return View(clientsList);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientModel input)
        {
            try
            {
                //if (!ModelState.IsValid) return View(input);

                var response = await _clientService.CreateClient(input);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction("Index", "Client");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

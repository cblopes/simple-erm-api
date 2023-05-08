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

        public async Task<IActionResult> Index(Guid? id)
        {
            var clients = await _clientService.GetClientsAsync();

            if (id != null)
            {
                var client = await _clientService.GetClientByIdAsync(id);

                clients = new List<ClientViewModel> { client };

                return View(clients);
            }

            return View(clients);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientModel input)
        {
            try
            {
                //if (!ModelState.IsValid) return View(input);

                var response = await _clientService.CreateClientAsync(input);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction("Index", "Client");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientViewModel input)
        {
            try
            {
                ModelState.Remove("CpfCnpj");

                AlterClientModel client = new AlterClientModel { Name = input.Name };

                var response = await _clientService.AlterClientAsync(id, client);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction("Index", "Client");
            }
            catch
            {
                return RedirectToAction("Index", "Client");
            }
        }

        // GET: ClientController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            return View(client);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, DeleteClientModel input)
        {
            try
            {
                ModelState.Remove("ResponseResult");

                var response = await _clientService.DeleteClientAsync(id);

                if (HasErrorsResponse(response.ResponseResult)) return View(response);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

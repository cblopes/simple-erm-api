using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.MVC.Models;
using SimpleERP.MVC.Services;

namespace SimpleERP.MVC.Controllers
{
    [Authorize]
    public class ClientController : MainController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index(string? document)
        {
            var clients = await _clientService.GetClientsAsync();

            if (document != null)
            {
                try
                {
                    var client = await _clientService.GetClientByDocumentAsync(document);

                    if (client.CpfCnpj == null) throw new HttpRequestException("Cliente não encontrado");

                    clients = new List<ClientViewModel> { client };

                    return View(clients);
                }
                catch (HttpRequestException ex)
                {
                    return View(clients);
                }
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
                var response = await _clientService.CreateClientAsync(input);

                if (HasErrorsResponse(response.ResponseResult)) return View(input);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(string document)
        {
            var client = await _clientService.GetClientByDocumentAsync(document);

            return View(client);
        }

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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string document)
        {
            var response = await _clientService.GetClientByDocumentAsync(document);

            var client = new DeleteClientModel
            {
                Id = response.Id,
                Name = response.Name,
                CpfCnpj = response.CpfCnpj
            };

            return View(client);
        }

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

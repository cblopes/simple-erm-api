using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientServices _clientServices;
        public ClientController(IMapper mapper, IClientServices clientServices)
        {
            _mapper = mapper;
            _clientServices = clientServices;
        }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <returns>Coleção de clientes</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientServices.GetAllClientsAsync();

            var clientViewModel = _mapper.ProjectTo<ClientViewModel>(clients.AsQueryable()).ToList();

            return Ok(clientViewModel);
        }

        /// <summary>
        /// Obter um cliente por Id
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Informações de um cliente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var client = await _clientServices.GetClientByIdAsync(id);

                var clientViewModel = _mapper.Map<ClientViewModel>(client);

                return Ok(clientViewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Cadastrar um cliente
        /// </summary>
        /// <param name="model">Dados do cliente</param>
        /// <returns>Cliente recém-cadastrado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateClientModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            try
            {
                var client = _mapper.Map<Client>(model);

                await _clientServices.CreateClientAsync(client);

                var clientViewModel = _mapper.Map<ClientViewModel>(client);

                return CreatedAtAction(nameof(GetById), new { id = clientViewModel.Id }, clientViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar os dados de um cliente
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="model">Dados do evento</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, UpdateClientModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);            

            try
            {
                var client = _mapper.Map<Client>(model);

                await _clientServices.UpdateClientAsync(id, client);

                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _clientServices.RemoveClientAsync(id);

                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}

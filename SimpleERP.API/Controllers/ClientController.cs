using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Entities;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [Route("api/v1/clients")]
    public class ClientController : MainController
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
            try
            {
                var clients = await _clientServices.GetAllClientsAsync();

                var clientViewModel = _mapper.ProjectTo<ClientViewModel>(clients.AsQueryable()).ToList();

                return Ok(clientViewModel);
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }

        /// <summary>
        /// Obter um cliente por Id
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Informações de um cliente</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var client = await _clientServices.GetClientByIdAsync(id);

                var clientViewModel = _mapper.Map<ClientViewModel>(client);

                return Ok(clientViewModel);
            }
            catch(Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
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
                AddProcessingError(ex.Message);
                return CustomResponse();
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, AlterClientModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);            

            try
            {
                var client = _mapper.Map<Client>(model);

                await _clientServices.UpdateClientAsync(id, client);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }            
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Sem retorno</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Má requisição</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _clientServices.RemoveClientAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                AddProcessingError(ex.Message);
                return CustomResponse();
            }
        }
    }
}

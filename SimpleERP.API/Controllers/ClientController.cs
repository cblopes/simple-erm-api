using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;
using SimpleERP.API.Models;

namespace SimpleERP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ErpDbContext _context;
        public ClientController(IMapper mapper, ErpDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <returns>Coleção de clientes</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var clients = _context.Clients.Where(c => c.IsActive).ToList();

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
        public IActionResult GetById(Guid id)
        { 
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            var clientViewModel = _mapper.Map<ClientViewModel>(client);

            return Ok(clientViewModel);
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
        public IActionResult Post(CreateClientModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var client = _mapper.Map<Client>(model);

            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = client.Id }, model);
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
        public IActionResult Put(Guid id, UpdateClientModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var client = _mapper.Map<Client>(model);

            client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            client.Update(model.Name);

            _context.Clients.Update(client);
            _context.SaveChanges();

            return NoContent();
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
        public IActionResult Delete(Guid id)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            client.Delete();

            _context.Clients.Update(client);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

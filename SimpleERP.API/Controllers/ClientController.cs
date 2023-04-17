using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.API.Data;
using SimpleERP.API.Entities;

namespace SimpleERP.API.Controllers
{
    [Route("api-simple-erp/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientDbContext _context;
        public ClientController(ClientDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _context.Clients.Where(c => c.IsActive).ToList();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        { 
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Client input)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            client.Update(input.Name);

            _context.Clients.Update(client);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
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

using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("Get_Cliente")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        
        [HttpGet("Get_ClienteById")]
        [Authorize]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

       
        [HttpPost("Create_Cliete")]
        [Authorize]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = cliente.Id },
                cliente
            );
        }

        
        [HttpPut("Upodate_Cliente")]
        [Authorize]
        public async Task<ActionResult> Put(int id, Cliente cliente)
        {
            var clienteBanco = await _context.Clientes.FindAsync(id);

            if (clienteBanco == null)
            {
                return NotFound();
            }

            clienteBanco.Nome = cliente.Nome;
            clienteBanco.Email = cliente.Email;
            clienteBanco.Telefone = cliente.Telefone;

            await _context.SaveChangesAsync();

            return Ok(clienteBanco);
        }

        
        [HttpDelete("Delete_ById")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
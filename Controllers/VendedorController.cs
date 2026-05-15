using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendedorController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("Get_Allvendedores")]
        public async Task<ActionResult<IEnumerable<Vendedor>>> Get()
        {
            return await _context.Vendedores.ToListAsync();
        }


        [HttpGet("Get_ById")]
        public async Task<ActionResult<Vendedor>> GetById(int codigo)
        {
            var vendedor = await _context.Vendedores.FindAsync(codigo);

            if (vendedor == null)
            {
                return NotFound();
            }

            return Ok(vendedor);
        }


        [HttpPost("Create_Vendedor")]
        public async Task<ActionResult> Post(Vendedor vendedor)
        {
            _context.Vendedores.Add(vendedor);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { codigo = vendedor.Id },
                vendedor
            );
        }


        [HttpPut("Update_Vendedor")]
        public async Task<ActionResult> Put(int codigo, Vendedor vendedor)
        {
            var vendedorBanco = await _context.Vendedores.FindAsync(codigo);

            if (vendedorBanco == null)
            {
                return NotFound();
            }

            vendedorBanco.Nome = vendedor.Nome;
            vendedorBanco.Email = vendedor.Email;
            vendedorBanco.Telefone = vendedor.Telefone;
            vendedorBanco.Salario = vendedor.Salario;

            await _context.SaveChangesAsync();

            return Ok(vendedorBanco);
        }

        [HttpGet("Get_BySalario")]
        public async Task<ActionResult<IEnumerable<Vendedor>>> BuscarPorSalario(decimal valor)
        {
            var vendedores = await _context.Vendedores
                .Where(x => x.Salario >= valor)
                .ToListAsync();

            if (!vendedores.Any())
            {
                return NotFound();
            }

            return Ok(vendedores);
        }


        [HttpDelete("Delete_Vendedor")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var vendedor = await _context.Vendedores.FindAsync(codigo);

            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedores.Remove(vendedor);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
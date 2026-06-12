using LojaApi.Data;
using LojaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FornecedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("Get_All")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> Get()
        {
            return await _context.Fornecedores.ToListAsync();
        }


        [HttpGet("Get_FornecedorById")]
        [Authorize]
        public async Task<ActionResult<Fornecedor>> GetById(int codigo)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(codigo);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }


        [HttpPost("Create_Forncedor")]
        [Authorize]
        public async Task<ActionResult> Post(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { codigo = fornecedor.Id },
                fornecedor
            );
        }


        [HttpPut("Update_Fornecedor")]
        [Authorize]
        public async Task<ActionResult> Put(int codigo, Fornecedor fornecedor)
        {
            var fornecedorBanco = await _context.Fornecedores.FindAsync(codigo);

            if (fornecedorBanco == null)
            {
                return NotFound();
            }

            fornecedorBanco.Nome = fornecedor.Nome;
            fornecedorBanco.Cnpj = fornecedor.Cnpj;
            fornecedorBanco.Email = fornecedor.Email;
            fornecedorBanco.Telefone = fornecedor.Telefone;

            await _context.SaveChangesAsync();

            return Ok(fornecedorBanco);
        }

        [HttpGet("Get_ByName")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> BuscarPorNome(string nome)
        {
            var fornecedores = await _context.Fornecedores
                .Where(x => x.Nome.Contains(nome))
                .ToListAsync();

            if (!fornecedores.Any())
            {
                return NotFound();
            }

            return Ok(fornecedores);
        }


        [HttpDelete("Delete_ById")]
        [Authorize]
        public async Task<ActionResult> Delete(int codigo)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(codigo);

            if (fornecedor == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedor);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
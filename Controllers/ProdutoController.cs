using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaApi.Data;
using LojaApi.Models;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get_AllProdutos")]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("Get_ProdutosById")]
        public async Task<ActionResult<Produto>> GetById(int codigo)
        {
            var produto = await _context.Produtos.FindAsync(codigo);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost("Create_Produto")]
        public async Task<ActionResult> Post(Produto produto)
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { codigo = produto.Id },
                produto
            );
        }

        [HttpPut("Update_Produto")]
        public async Task<ActionResult> Put(int codigo, Produto produto)
        {
            var produtoBanco = await _context.Produtos.FindAsync(codigo);

            if (produtoBanco == null)
            {
                return NotFound();
            }

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Preco = produto.Preco;

            await _context.SaveChangesAsync();

            return Ok(produtoBanco);
        }

        [HttpDelete("Delete_Produto")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var produto = await _context.Produtos.FindAsync(codigo);

            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaApi.Data;
using SalasReuniaoApi.Models;

namespace SalasReuniaoApi.Controllers;

[ApiController]
[Route("api/salas")]
[Authorize] // todas as rotas deste controller exigem JWT válido
public class SalasReuniaoController : ControllerBase
{
    private readonly AppDbContext _db;

    public SalasReuniaoController(AppDbContext db)
    {
        _db = db;
    }

    // GET /salas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaReuniao>>> GetAll()
    {
        var salas = await _db.SalasReuniao.AsNoTracking().ToListAsync();
        return Ok(salas);
    }

   
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SalaReuniao>> GetById(int id)
    {
        var sala = await _db.SalasReuniao.FindAsync(id);

        if (sala is null)
            return NotFound();

        return Ok(sala);
    }

    
    [HttpPost]
    public async Task<ActionResult<SalaReuniao>> Create([FromBody] SalaReuniao sala)
    {
        if (string.IsNullOrWhiteSpace(sala.Nome))
            return BadRequest("O campo 'Nome' é obrigatório.");

   
        sala.Id = 0;

        _db.SalasReuniao.Add(sala);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = sala.Id }, sala);
    }

    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<SalaReuniao>> Update(int id, [FromBody] SalaReuniao salaAtualizada)
    {
        var sala = await _db.SalasReuniao.FindAsync(id);

        if (sala is null)
            return NotFound();

        if (string.IsNullOrWhiteSpace(salaAtualizada.Nome))
            return BadRequest("O campo 'Nome' é obrigatório.");

        
        sala.Nome = salaAtualizada.Nome;
        sala.Capacidade = salaAtualizada.Capacidade;
        sala.PossuiProjetor = salaAtualizada.PossuiProjetor;

        await _db.SaveChangesAsync();

        return Ok(sala);
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sala = await _db.SalasReuniao.FindAsync(id);

        if (sala is null)
            return NotFound();

        _db.SalasReuniao.Remove(sala);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorPagosApi.Models.Entities;

namespace GestorPagosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoUsuariosController : ControllerBase
    {
        private readonly ClubDeportivoContext _context;

        public AutoUsuariosController(ClubDeportivoContext context)
        {
            _context = context;
        }

        // GET: api/AutoUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var equis = await _context.Usuarios.ToListAsync();
            return equis;
        }

        // GET: api/AutoUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // PUT: api/AutoUsuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AutoUsuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'ClubDeportivoContext.Usuarios'  is null.");
            }
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.IdUsuario }, usuarios);
        }

        // DELETE: api/AutoUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}

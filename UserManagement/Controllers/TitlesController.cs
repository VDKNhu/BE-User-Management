using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/titles")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly UserManagementContext _context;

        public TitlesController(UserManagementContext context)
        {
            this._context = context;
        }

        // GET: api/Titles
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
          if (this._context.Titles == null)
          {
              return NotFound();
          }
            return await this._context.Titles
                .Include(ttl => ttl.WebUsers)
                .ToListAsync();
        }

        // GET: api/Titles/5
        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> GetTitle(int id)
        {
          if (this._context.Titles == null)
          {
              return NotFound();
          }
            var title = await this._context.Titles.FindAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        // GET: api/Titles/5
        [EnableCors("AllowOrigin")]
        [HttpGet("detail-title/{id}")]
        public async Task<ActionResult<Title>> GetDetailTitle(int id)
        {
            if (this._context.Titles == null)
            {
                return NotFound();
            }
            var title = this._context.Titles
                .Include(ttl => ttl.WebUsers)
                .Where(ttl => ttl.Id == id)
                .FirstOrDefault();

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        // PUT: api/Titles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTitle(int id, Title title)
        {
            if (id != title.Id)
            {
                return BadRequest();
            }

            this._context.Entry(title).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
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

        // POST: api/Titles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async Task<ActionResult<Title>> PostTitle(Title title)
        {
          if (this._context.Titles == null)
          {
              return Problem("Entity set 'UserManagementContext.Titles'  is null.");
          }

          this._context.Titles.Add(title);
          await this._context.SaveChangesAsync();

          return CreatedAtAction("GetTitle", new { id = title.Id }, title);
        }

        // DELETE: api/Titles/5
        [EnableCors("AllowOrigin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTitle(int id)
        {
            if (this._context.Titles == null)
            {
                return NotFound();
            }
            var title = await this._context.Titles.FindAsync(id);
            if (title == null)
            {
                return NotFound();
            }

            this._context.Titles.Remove(title);
            await this._context.SaveChangesAsync();

            return NoContent();
        }

        private bool TitleExists(int id)
        {
            return (this._context.Titles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

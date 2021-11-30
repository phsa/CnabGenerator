using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CnabGenerator.Models;

namespace CnabGenerator.Controllers
{
    [ApiController]
    [Route("api/IssuingInstitution")]
    public class IssuingInstitutionController : ControllerBase
    {
        private readonly CnabGeneratorContext _context;

        public IssuingInstitutionController(CnabGeneratorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssuingInstitution>>> GetIssuingInstitutons()
        {
            return await _context.IssuingInstitutions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssuingInstitution>> GetIssuingInstituton(long id)
        {
            var issuingInstituton = await _context.IssuingInstitutions.FindAsync(id);

            if (issuingInstituton == null)
            {
                return NotFound();
            }

            return issuingInstituton;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssuingInstituton(long id, IssuingInstitution issuingInstituton)
        {
            if (id != issuingInstituton.Id)
            {
                return BadRequest();
            }

            _context.Entry(issuingInstituton).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssuingInstitutonExists(id))
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

        [HttpPost]
        public async Task<ActionResult<IssuingInstitution>> PostIssuingInstituton(IssuingInstitution issuingInstituton)
        {
            _context.IssuingInstitutions.Add(issuingInstituton);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIssuingInstituton).ToLower(), new { id = issuingInstituton.Id }, issuingInstituton);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssuingInstituton(long id)
        {
            var issuingInstituton = await _context.IssuingInstitutions.FindAsync(id);
            if (issuingInstituton == null)
            {
                return NotFound();
            }

            _context.IssuingInstitutions.Remove(issuingInstituton);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssuingInstitutonExists(long id)
        {
            return _context.IssuingInstitutions.Any(e => e.Id == id);
        }

        //private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        //    new TodoItemDTO
        //    {
        //        Id = todoItem.Id,
        //        Name = todoItem.Name,
        //        IsComplete = todoItem.IsComplete
        //    };
    }
}

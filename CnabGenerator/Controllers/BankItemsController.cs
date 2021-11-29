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
    [Route("api/BankItems")]
    [ApiController]
    public class BankItemsController : ControllerBase
    {
        private readonly CnabGeneratorContext _context;

        public BankItemsController(CnabGeneratorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankItem>>> GetBankItems()
        {
            return await _context.BankItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankItem>> GetBankItem(long id)
        {
            var bankItem = await _context.BankItems.FindAsync(id);

            if (bankItem == null)
            {
                return NotFound();
            }

            return bankItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankItem(long id, BankItem bankItem)
        {
            if (id != bankItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(bankItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankItemExists(id))
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
        public async Task<ActionResult<BankItem>> PostBankItem(BankItem bankItem)
        {
            _context.BankItems.Add(bankItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBankItem).ToLower(), new { id = bankItem.Id }, bankItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankItem(long id)
        {
            var bankItem = await _context.BankItems.FindAsync(id);
            if (bankItem == null)
            {
                return NotFound();
            }

            _context.BankItems.Remove(bankItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankItemExists(long id)
        {
            return _context.BankItems.Any(e => e.Id == id);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Data;
using EmployeeService.Models;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryContext _context;

        public InventoryController(InventoryContext context)
        {
            _context = context;
        }

        // GET: api/Inventorys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventoryList()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventorys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var Inventory = await _context.Inventories.FindAsync(id);

            if (Inventory == null)
            {
                return NotFound();
            }

            return Inventory;
        }

        // Update: api/Inventorys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory Inventory)
        {
            if (id != Inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(Inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventorys
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory Inventory)
        {
            _context.Inventories.Add(Inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = Inventory.Id }, Inventory);
        }

        // DELETE: api/Inventorys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var Inventory = await _context.Inventories.FindAsync(id);
            if (Inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(Inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}

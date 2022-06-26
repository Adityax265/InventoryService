using Microsoft.EntityFrameworkCore;
namespace EmployeeService.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext (DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }
        public DbSet<EmployeeService.Models.Inventory> Inventories { get; set; }
    }
}

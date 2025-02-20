using Enterprise_Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_Expense_Tracker.ModelContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}

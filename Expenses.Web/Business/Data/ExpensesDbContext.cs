using Microsoft.EntityFrameworkCore;

namespace Expenses.Web.Business.Data
{
  public class ExpensesDbContext : DbContext
  {
    public DbSet<User> UserSet { get; set; }

    public DbSet<Category> CategorySet { get; set; }

    public DbSet<Account> AccountSet { get; set; }

    public DbSet<Transaction> TransactionSet { get; set; }

    public DbSet<StandingOrder> StandingOrderSet { get; set; }

    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> dbContextOptions)
       : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}

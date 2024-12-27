using Microsoft.EntityFrameworkCore;
using Seylan_RED_BackEND.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public DbSet<UserDetails> SeylanREDUsers { get; set; }
}


using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Data
{
    public class SellDomainContext:DbContext
    {
    public SellDomainContext(DbContextOptions<SellDomainContext> options):base(options)
    {

    }
    public DbSet<SellDomain> SellDomains { get; set; }


    }
}

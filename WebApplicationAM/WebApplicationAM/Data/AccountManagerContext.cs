using Microsoft.EntityFrameworkCore;

namespace WebApplicationAM.Data
{
    public class AccountManagerContext(DbContextOptions<AccountManagerContext> options) 
        : DbContext(options)
    {
        public DbSet<WebApplicationAM.Model.User> Users => Set<WebApplicationAM.Model.User>();

    }
}

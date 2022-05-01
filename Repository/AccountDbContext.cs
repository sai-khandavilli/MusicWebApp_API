using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicWebApp.Repository
{
    public class AccountDbContext : IdentityDbContext
    {
        public AccountDbContext (DbContextOptions<AccountDbContext> options)
            : base(options)
        {
        }
    }
}

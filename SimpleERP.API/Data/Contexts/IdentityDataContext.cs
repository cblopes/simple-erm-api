using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpleERP.API.Data.Contexts
{
    public class IdentityDataContext : IdentityDbContext
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }
    }
}

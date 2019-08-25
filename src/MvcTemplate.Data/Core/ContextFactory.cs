using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MvcTemplate.Data.Core
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            //optionsBuilder.UseSqlite("Data Source=MvcTemplate.db");
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MvcTemplate;Trusted_Connection=True;MultipleActiveResultSets=True");

            return new Context(optionsBuilder.Options, null);
        }
    }
}

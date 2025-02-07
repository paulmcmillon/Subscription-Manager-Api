using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Subscriptions.Data
{
    internal class SubscriptionsContextFactory : IDesignTimeDbContextFactory<SubscriptionsContext>
    {
        public SubscriptionsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SubscriptionsContext>();
            optionsBuilder.UseSqlServer("Server=tcp:softeknology-sql-server.database.windows.net,1433;Initial Catalog=subscription-manager;Persist Security Info=False;User ID=softeknology-sql-admin;Password=Pmcmillon1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer("Data Source=LAPTOP-5NBB4HF0;Initial Catalog=subscription-manager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new SubscriptionsContext(optionsBuilder.Options);
        }
    }
}

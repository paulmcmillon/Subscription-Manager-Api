using Microsoft.EntityFrameworkCore;
using Subscriptions.Data.Models;

namespace Subscriptions.Data
{
    public sealed class SubscriptionsContext(DbContextOptions<SubscriptionsContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SubscriptionConfiguration().Configure(modelBuilder.Entity<Subscription>());
            new SubscriptionTypeConfiguration().Configure(modelBuilder.Entity<SubscriptionType>());
            new PaymentConfiguration().Configure(modelBuilder.Entity<Payment>());

            //SeedDatabaseAsync();
        }

        // TODO: Test async seeding
        // TODO: Move to separate class
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //.UseAsyncSeeding(async (context, isDevelopment, cancellationToken) =>
            //{
            //    var SubscriptionLight = new SubscriptionType()
            //    {
            //        Name = "GroundzKeeper Light",
            //        Description = "Description for a basic level subscription",
            //        PriceMonthly = 9.99m,
            //        PriceYearly = 99.99m,
            //    };
            //    await context.AddAsync(SubscriptionLight, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);


            //    var FeaturesLight = new List<Feature>
            //        {
            //            new Feature { SubscriptionTypeId = SubscriptionLight.Id, Name = "Customer Management", Description = "Description 1" },
            //            new Feature { SubscriptionTypeId = SubscriptionLight.Id, Name = "Billing", Description = "Description 2" },
            //            new Feature { SubscriptionTypeId = SubscriptionLight.Id, Name = "Job Scheduling", Description = "Description 3" }
            //        };
            //    await context.AddRangeAsync(FeaturesLight, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);


            //    var SubscriptionPro = new SubscriptionType()
            //    {
            //        Name = "GroundzKeeper Pro",
            //        Description = "Description for a pro level subscription",
            //        PriceMonthly = 49.99m,
            //        PriceYearly = 539.99m,
            //    };
            //    await context.AddAsync(SubscriptionPro, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);


            //    var FeaturesPro = new List<Feature>
            //        {
            //            new Feature { SubscriptionTypeId = SubscriptionPro.Id, Name = "Customer Management", Description = "Description 1" },
            //            new Feature { SubscriptionTypeId = SubscriptionPro.Id, Name = "Billing", Description = "Description 2" },
            //            new Feature { SubscriptionTypeId = SubscriptionPro.Id, Name = "Job Scheduling", Description = "Description 3" },
            //            new Feature { SubscriptionTypeId = SubscriptionPro.Id, Name = "Inventory", Description = "Description 4" },
            //            new Feature { SubscriptionTypeId = SubscriptionPro.Id, Name = "Mobile App", Description = "Description 5" }
            //        };
            //    await context.AddRangeAsync(FeaturesPro, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);


            //    var SubscriptionEnterprise = new SubscriptionType()
            //    {
            //        Name = "GroundzKeeper Enterprise",
            //        Description = "Description for an enterprise level subscription",
            //        PriceMonthly = 29.99m,
            //        PriceYearly = 299.99m,
            //    };
            //    await context.AddAsync(SubscriptionEnterprise, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);

            //    var FeaturesEnterprise = new List<Feature>
            //        {
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Customer Management", Description = "Description 1" },
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Billing", Description = "Description 2" },
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Job Scheduling", Description = "Description 3" },
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Inventory", Description = "Description 4" },
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Mobile App", Description = "Description 5" },
            //            new Feature { SubscriptionTypeId = SubscriptionEnterprise.Id, Name = "Reporting", Description = "Description 6" }
            //        };
            //    await context.AddRangeAsync(FeaturesEnterprise, cancellationToken);
            //    await context.SaveChangesAsync(cancellationToken);
            //});

            SeedDatabaseAsync();
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feature> Features { get; set; }

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Populate records in the database. These records are typically static data
        /// that does not change.
        /// </summary>
        /// <returns></returns>
        //------------------------------------------------------------------------------------
        public async Task SeedDatabaseAsync()
        {
            //SubscriptionType subscriptionType = null;
            //Feature[] features = null;

            if (!await SubscriptionTypes.AnyAsync())
            {
                var SubscriptionLight = new SubscriptionType()
                {
                    Name = "GroundzKeeper Light",
                    Description = "Description for a basic level subscription",
                    PriceMonthly = 9.99m,
                    PriceYearly = 99.99m,
                };
                await SubscriptionTypes.AddAsync(SubscriptionLight);
                await SaveChangesAsync();
            }
        }
    }
}

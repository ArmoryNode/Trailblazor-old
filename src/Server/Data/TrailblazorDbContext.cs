using Microsoft.EntityFrameworkCore;
using Trailblazor.Server.Models.Data;

namespace Trailblazor.Server.Data
{
    public class TrailblazorDbContext : DbContext
    {
        public DbSet<GearList> GearLists => Set<GearList>();

        public TrailblazorDbContext(DbContextOptions<TrailblazorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GearList>(g =>
            {
                // Configure container name to match DbSet property name
                g.ToContainer(nameof(GearLists));

                // We are only going to be storing gear lists in this container, so no need for a discriminator
                g.HasNoDiscriminator();

                // Fix casing during deserialization
                g.Property(x => x.Id).ToJsonProperty("id").IsRequired(true);

                // Configure the "OwnerId" as the partition key for the document and specify a conversion
                // https://docs.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli#partition-keys
                g.HasPartitionKey(x => x.OwnerId);
                //g.Property(x => x.OwnerId).HasConversion<string>();

                // Only return entities that have not been soft deleted by default
                g.HasQueryFilter(x => x.Deleted == null);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            MarkSoftDelted();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            MarkSoftDelted();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void MarkSoftDelted()
        {
            // Get all tracked entries with entities of type `ISoftDeletable`
            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                // Only modify entities originally marked for deletion
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified; // Mark soft deletable entity for modification, instead of deletion
                    entry.Entity.Deleted = DateTimeOffset.UtcNow; // Set the `Deleted` property
                }
            }
        }

        public static async Task InitializeDbContext(DbContextOptions<TrailblazorDbContext> options, CancellationToken cancellationToken = default)
        {
            using var context = new TrailblazorDbContext(options);

            await context.Database.EnsureCreatedAsync(cancellationToken);

            context.Database.GetCosmosClient();
        }
    }
}
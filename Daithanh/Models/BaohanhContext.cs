using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Daithanh.Models.Mapping;

namespace Daithanh.Models
{
    public partial class BaohanhContext : DbContext
    {
        static BaohanhContext()
        {
            Database.SetInitializer<BaohanhContext>(null);
        }

        public BaohanhContext()
            : base("Name=BaohanhContext")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Distribute> Distributes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Village> Villages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistributeMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            modelBuilder.Configurations.Add(new VillageMap());
        }
    }
}

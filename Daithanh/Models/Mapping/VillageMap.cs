using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class VillageMap : EntityTypeConfiguration<Village>
    {
        public VillageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Village");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.DistributeId).HasColumnName("DistributeId");

            // Relationships
            this.HasOptional(t => t.Distribute)
                .WithMany(t => t.Villages)
                .HasForeignKey(d => d.DistributeId);
            this.HasOptional(t => t.District)
                .WithMany(t => t.Villages)
                .HasForeignKey(d => d.DistrictId);

        }
    }
}

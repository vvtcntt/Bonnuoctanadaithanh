using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("District");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceId");
            this.Property(t => t.DistributeId).HasColumnName("DistributeId");

            // Relationships
            this.HasOptional(t => t.Distribute)
                .WithMany(t => t.Districts)
                .HasForeignKey(d => d.DistributeId);
            this.HasOptional(t => t.Province)
                .WithMany(t => t.Districts)
                .HasForeignKey(d => d.ProvinceId);

        }
    }
}

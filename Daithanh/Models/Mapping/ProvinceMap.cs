using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Province");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DistributeId).HasColumnName("DistributeId");
            this.Property(t => t.CountryId).HasColumnName("CountryId");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Provinces)
                .HasForeignKey(d => d.CountryId);
            this.HasOptional(t => t.Distribute)
                .WithMany(t => t.Provinces)
                .HasForeignKey(d => d.DistributeId);

        }
    }
}

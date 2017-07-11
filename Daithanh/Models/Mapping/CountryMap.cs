using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ISO)
                .HasMaxLength(10);

            this.Property(t => t.Code)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Country");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ISO).HasColumnName("ISO");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Default).HasColumnName("Default");
        }
    }
}

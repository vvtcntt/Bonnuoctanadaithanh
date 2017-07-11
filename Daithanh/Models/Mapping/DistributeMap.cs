using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class DistributeMap : EntityTypeConfiguration<Distribute>
    {
        public DistributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Explain)
                .HasMaxLength(50);

            this.Property(t => t.Display)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Distribute");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Explain).HasColumnName("Explain");
            this.Property(t => t.Display).HasColumnName("Display");
        }
    }
}

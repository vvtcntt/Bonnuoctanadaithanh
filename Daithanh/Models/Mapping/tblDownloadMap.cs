using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Daithanh.Models.Mapping
{
    public class tblDownloadMap : EntityTypeConfiguration<tblDownload>
    {
        public tblDownloadMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(500);

            this.Property(t => t.ImageName)
                .HasMaxLength(500);

            this.Property(t => t.ImageLink)
                .HasMaxLength(500);

            this.Property(t => t.ImageLinkRoot)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("tblDownload");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.HeadShort).HasColumnName("HeadShort");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ImageLink).HasColumnName("ImageLink");
            this.Property(t => t.ImageLinkRoot).HasColumnName("ImageLinkRoot");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.idUser).HasColumnName("idUser");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
        }
    }
}
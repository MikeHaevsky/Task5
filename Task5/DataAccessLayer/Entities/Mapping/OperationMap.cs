using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Entities.Mapping
{
    public class OperationMap : EntityTypeConfiguration<Operation>
    {
        public OperationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Operations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.ManagerId).HasColumnName("ManagerId");
            this.Property(t => t.ClientId).HasColumnName("ClientId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Cost).HasColumnName("Cost");

            // Relationships
            this.HasRequired(t => t.Client)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.ClientId);
            this.HasRequired(t => t.Manager)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.ManagerId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.Operations)
                .HasForeignKey(d => d.ProductId);

        }
    }
}

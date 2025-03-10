using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WMAPI.Models
{
    public partial class WarehouseManagementContext : DbContext
    {
        public WarehouseManagementContext()
        {
        }

        public WarehouseManagementContext(DbContextOptions<WarehouseManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<WarehouseIn> WarehouseIns { get; set; } = null!;
        public virtual DbSet<WarehouseInDetail> WarehouseInDetails { get; set; } = null!;
        public virtual DbSet<WarehouseOut> WarehouseOuts { get; set; } = null!;
        public virtual DbSet<WarehouseOutDetail> WarehouseOutDetails { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=SOL\\SOL;database=WarehouseManagement;user=sa;password=123");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.HasIndex(e => e.ProductId, "UQ__Inventor__47027DF49CD1E4CC")
                    .IsUnique();

                entity.Property(e => e.InventoryId).HasColumnName("inventory_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("last_updated");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Inventory)
                    .HasForeignKey<Inventory>(d => d.ProductId)
                    .HasConstraintName("FK__Inventory__produ__46E78A0C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .HasColumnName("product_name");
            });

            modelBuilder.Entity<WarehouseIn>(entity =>
            {
                entity.HasKey(e => e.InId)
                    .HasName("PK__Warehous__1CD08BE92FB4B6B7");

                entity.ToTable("Warehouse_In");

                entity.HasIndex(e => e.InCode, "UQ__Warehous__CE5B7C52C81BDB3B")
                    .IsUnique();

                entity.Property(e => e.InId).HasColumnName("in_id");

                entity.Property(e => e.InCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("in_code");

                entity.Property(e => e.InDate)
                    .HasColumnType("datetime")
                    .HasColumnName("in_date");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(255)
                    .HasColumnName("supplier_name");

                entity.Property(e => e.TotalPriceIn)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("total_price_in");
            });

            modelBuilder.Entity<WarehouseInDetail>(entity =>
            {
                entity.HasKey(e => e.InDetailId)
                    .HasName("PK__Warehous__8F4C75479CB3C10D");

                entity.ToTable("Warehouse_In_Details");

                entity.Property(e => e.InDetailId).HasColumnName("in_detail_id");

                entity.Property(e => e.InId).HasColumnName("in_id");

                entity.Property(e => e.PriceIn)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("price_in");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.QuantityIn).HasColumnName("quantity_in");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.In)
                    .WithMany(p => p.WarehouseInDetails)
                    .HasForeignKey(d => d.InId)
                    .HasConstraintName("FK__Warehouse__in_id__3B75D760");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseInDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Warehouse__produ__3C69FB99");
            });

            modelBuilder.Entity<WarehouseOut>(entity =>
            {
                entity.HasKey(e => e.OutId)
                    .HasName("PK__Warehous__D7CC77D8BB671953");

                entity.ToTable("Warehouse_Out");

                entity.HasIndex(e => e.OutCode, "UQ__Warehous__BD9478994FAED0CD")
                    .IsUnique();

                entity.Property(e => e.OutId).HasColumnName("out_id");

                entity.Property(e => e.OutCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("out_code");

                entity.Property(e => e.OutDate)
                    .HasColumnType("datetime")
                    .HasColumnName("out_date");

                entity.Property(e => e.RecipientName)
                    .HasMaxLength(255)
                    .HasColumnName("recipient_name");

                entity.Property(e => e.TotalPriceOut)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("total_price_out");
            });

            modelBuilder.Entity<WarehouseOutDetail>(entity =>
            {
                entity.HasKey(e => e.OutDetailId)
                    .HasName("PK__Warehous__0DC07A7F48CAB686");

                entity.ToTable("Warehouse_Out_Details");

                entity.Property(e => e.OutDetailId).HasColumnName("out_detail_id");

                entity.Property(e => e.OutId).HasColumnName("out_id");

                entity.Property(e => e.PriceOut)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("price_out");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.QuantityOut).HasColumnName("quantity_out");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Out)
                    .WithMany(p => p.WarehouseOutDetails)
                    .HasForeignKey(d => d.OutId)
                    .HasConstraintName("FK__Warehouse__out_i__4222D4EF");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseOutDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Warehouse__produ__4316F928");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

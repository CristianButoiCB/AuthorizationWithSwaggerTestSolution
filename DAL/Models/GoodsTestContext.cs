using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class GoodsTestContext : DbContext
    {
        public GoodsTestContext()
        {
        }

        public GoodsTestContext(DbContextOptions<GoodsTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bptype> Bptypes { get; set; } = null!;
        public virtual DbSet<BusinessPartner> BusinessPartners { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrdersLine> PurchaseOrdersLines { get; set; } = null!;
        public virtual DbSet<SaleOrder> SaleOrders { get; set; } = null!;
        public virtual DbSet<SaleOrdersLine> SaleOrdersLines { get; set; } = null!;
        public virtual DbSet<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CRISTILAPTOP11\\CRISTILAPTOP1112;Initial Catalog=GoodsTest;user id=sn;password=SN1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bptype>(entity =>
            {
                entity.HasKey(e => e.TypeCode);

                entity.ToTable("BPType");

                entity.Property(e => e.TypeCode).HasMaxLength(1);

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<BusinessPartner>(entity =>
            {
                entity.HasKey(e => e.Bpcode);

                entity.Property(e => e.Bpcode)
                    .HasMaxLength(128)
                    .HasColumnName("BPCode");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Bpname)
                    .HasMaxLength(254)
                    .HasColumnName("BPName");

                entity.Property(e => e.Bptype)
                    .HasMaxLength(1)
                    .HasColumnName("BPType");

                entity.HasOne(d => d.BptypeNavigation)
                    .WithMany(p => p.BusinessPartners)
                    .HasForeignKey(d => d.Bptype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessPartners_BPType");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.Property(e => e.ItemCode).HasMaxLength(128);

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemName).HasMaxLength(254);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bpcode)
                    .HasMaxLength(128)
                    .HasColumnName("BPCode");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.BpcodeNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.Bpcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrders_BusinessPartners");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PurchaseOrderCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrders_Users");

                entity.HasOne(d => d.LastUpdatedByNavigation)
                    .WithMany(p => p.PurchaseOrderLastUpdatedByNavigations)
                    .HasForeignKey(d => d.LastUpdatedBy)
                    .HasConstraintName("FK_PurchaseOrders_Users1");
            });

            modelBuilder.Entity<PurchaseOrdersLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.ItemCode).HasMaxLength(128);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PurchaseOrdersLineCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrdersLines_Users");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.PurchaseOrdersLines)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrdersLines_PurchaseOrdersLines");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.PurchaseOrdersLines)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrdersLines_Items");

                entity.HasOne(d => d.LastUpdatedByNavigation)
                    .WithMany(p => p.PurchaseOrdersLineLastUpdatedByNavigations)
                    .HasForeignKey(d => d.LastUpdatedBy)
                    .HasConstraintName("FK_PurchaseOrdersLines_Users1");
            });

            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bpcode)
                    .HasMaxLength(128)
                    .HasColumnName("BPCode");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.BpcodeNavigation)
                    .WithMany(p => p.SaleOrders)
                    .HasForeignKey(d => d.Bpcode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrders_BusinessPartners");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.SaleOrderCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrders_Users");

                entity.HasOne(d => d.LastUpdatedByNavigation)
                    .WithMany(p => p.SaleOrderLastUpdatedByNavigations)
                    .HasForeignKey(d => d.LastUpdatedBy)
                    .HasConstraintName("FK_SaleOrders_Users1");
            });

            modelBuilder.Entity<SaleOrdersLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.ItemCode).HasMaxLength(128);

                entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnType("decimal(38, 18)");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.SaleOrdersLines)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrdersLines_SaleOrders");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.SaleOrdersLines)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrdersLines_Items");
            });

            modelBuilder.Entity<SaleOrdersLinesComment>(entity =>
            {
                entity.HasKey(e => e.CommentLineId);

                entity.Property(e => e.CommentLineId).HasColumnName("CommentLineID");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.SaleOrdersLinesComments)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrdersLinesComments_SaleOrdersLinesComments");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.SaleOrdersLinesComments)
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleOrdersLinesComments_SaleOrdersLines");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasIndex(e => e.UserName, "ClusteredIndex-20240125-102856")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FullName).HasMaxLength(1024);

                entity.Property(e => e.UserName).HasMaxLength(254);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

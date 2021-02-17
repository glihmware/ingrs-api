using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ingrs.Access
{
    public partial class DbCtx : DbContext
    {
        public DbCtx()
        {
        }

        public DbCtx(DbContextOptions<DbCtx> options)
            : base(options)
        {
        }

        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<Op> Ops { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=1234;database=ingrs", Microsoft.EntityFrameworkCore.ServerVersion.FromString("10.5.8-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>(entity =>
            {
                entity.ToTable("Domain");

                entity.HasIndex(e => e.Iid, "IID")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EcdsaPub)
                    .HasColumnType("varchar(512)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Iid)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasColumnName("IID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.RsaPub)
                    .HasColumnType("varchar(1024)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Op>(entity =>
            {
                entity.ToTable("Op");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DomainId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.GenerateTs)
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RandNum).HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Tiid)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasColumnName("TIID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.Type).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Argon22, "Argon22")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Argon22)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.DomainId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.FailAttempts)
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Iid)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasColumnName("IID")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.LastSuccessCon)
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RegisterTs)
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnType("varchar(512)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_unicode_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

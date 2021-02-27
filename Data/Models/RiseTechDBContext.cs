using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Data.Models
{
    public partial class RiseTechDBContext : DbContext
    {
        public RiseTechDBContext()
        {
        }

        public RiseTechDBContext(DbContextOptions<RiseTechDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Directory> Directories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning Eğer bilgisayarda kurulu olan local SQL sunucusunun instance olarak isim verilmediyse 'Server=.' olarak düzenlenmesi gerekebilir.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RiseTechDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts_Directories");
            });

            modelBuilder.Entity<Directory>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK_Directory");

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

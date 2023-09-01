using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2;

public partial class StudentContext : DbContext
{
    public StudentContext()
    {
        Database.EnsureCreated();
    }

    public StudentContext(DbContextOptions<StudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<CityTable> CityTables { get; set; }

    public virtual DbSet<Curriculum> Curriculums { get; set; }

    public virtual DbSet<FacultyTable> FacultyTables { get; set; }

    public virtual DbSet<PostindexTable> PostindexTables { get; set; }

    public virtual DbSet<SpecialityTable> SpecialityTables { get; set; }

    public virtual DbSet<StreetTable> StreetTables { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StudentData;Username=postgres;Password=1423");
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Student;Username=postgres;Password=1423");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address");

            entity.HasIndex(e => new { e.CityId, e.PostindexId, e.StreetId }, "address_cityId_postindexId_streetId_key").IsUnique();

            entity.HasData(
                new Address { Id = 1, CityId = 1, StreetId = 2, PostindexId = 1 },
                new Address { Id = 2, CityId = 1, StreetId = 2, PostindexId = 3 },
                new Address { Id = 3, CityId = 2, StreetId = 1, PostindexId = 2 },
                new Address { Id = 4, CityId = 1, StreetId = 1, PostindexId = 3 },
                new Address { Id = 5, CityId = 1, StreetId = 1, PostindexId = 1 },
                new Address { Id = 6, CityId = 1, StreetId = 4, PostindexId = 4 },
                new Address { Id = 7, CityId = 3, StreetId = 3, PostindexId = 5 }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.PostindexId).HasColumnName("postindexId");
            entity.Property(e => e.StreetId).HasColumnName("streetId");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_cityId_fkey");

            entity.HasOne(d => d.Postindex).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PostindexId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_postindexId_fkey");

            entity.HasOne(d => d.Street).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_streetId_fkey");
        });

        modelBuilder.Entity<CityTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cityTable_pkey");

            entity.ToTable("cityTable");

            entity.HasIndex(e => e.City, "cityTable_city_key").IsUnique();

            entity.HasData(
                new CityTable { Id = 1, City = "Ставрополь" },
                new CityTable { Id = 2, City = "Москва" },
                new CityTable { Id = 3, City = "Нижний новгород" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("curriculum_pkey");

            entity.ToTable("curriculum");

            entity.HasIndex(e => new { e.FacultyId, e.SpecialityId, e.Course, e.Group }, "curriculum_facultyId_specialityId_course_group_key").IsUnique();

            entity.HasData(
                new Curriculum { Id = 1, FacultyId = 1, SpecialityId = 1, Course = "5", Group = "КМБ-с-о-19-1" },
                new Curriculum { Id = 2, FacultyId = 2, SpecialityId = 3, Course = "3", Group = "ФИЗ-б-о-21-1" },
                new Curriculum { Id = 3, FacultyId = 1, SpecialityId = 2, Course = "4", Group = "ИВТ-б-о-20-1" },
                new Curriculum { Id = 4, FacultyId = 1, SpecialityId = 4, Course = "1", Group = "ИНБ-м-о-22-1" },
                new Curriculum { Id = 5, FacultyId = 2, SpecialityId = 3, Course = "1", Group = "ФИЗ-б-о-23-1" },
                new Curriculum { Id = 6, FacultyId = 2, SpecialityId = 3, Course = "2", Group = "ФИЗ-б-о-22-1" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.FacultyId).HasColumnName("facultyId");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.SpecialityId).HasColumnName("specialityId");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("curriculum_facultyId_fkey");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("curriculum_specialityId_fkey");
        });

        modelBuilder.Entity<FacultyTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("facultyTable_pkey");

            entity.ToTable("facultyTable");

            entity.HasIndex(e => e.Faculty, "facultyTable_faculty_key").IsUnique();

            entity.HasData(
                new FacultyTable { Id = 1, Faculty = "Институт цифрового развития" },
                new FacultyTable { Id = 2, Faculty = "Институт математики и естественных наук" },
                new FacultyTable { Id = 3, Faculty = "Гуманитарный институт" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Faculty).HasColumnName("faculty");
        });

        modelBuilder.Entity<PostindexTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("postindexTable_pkey");

            entity.ToTable("postindexTable");

            entity.HasIndex(e => e.PostIndex, "postindexTable_postIndex_key").IsUnique();

            entity.HasData(
                new PostindexTable { Id = 1, PostIndex = "134406" },
                new PostindexTable { Id = 2, PostIndex = "311206" },
                new PostindexTable { Id = 3, PostIndex = "300209" },
                new PostindexTable { Id = 4, PostIndex = "326009" },
                new PostindexTable { Id = 5, PostIndex = "316006" },
                new PostindexTable { Id = 6, PostIndex = "318006" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostIndex).HasColumnName("postIndex");
        });

        modelBuilder.Entity<SpecialityTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialityTable_pkey");

            entity.ToTable("specialityTable");

            entity.HasIndex(e => e.Speciality, "specialityTable_speciality_key").IsUnique();

            entity.HasData(
                new SpecialityTable { Id = 1, Speciality = "Компьютерная безопасность" },
                new SpecialityTable { Id = 2, Speciality = "Информатика и вычислительная техника" },
                new SpecialityTable { Id = 3, Speciality = "Физика" },
                new SpecialityTable { Id = 4, Speciality = "Информационная безопасность" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Speciality).HasColumnName("speciality");
        });

        modelBuilder.Entity<StreetTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("streetTable_pkey");

            entity.ToTable("streetTable");

            entity.HasIndex(e => e.Street, "streetTable_street_key").IsUnique();

            entity.HasData(
                new StreetTable { Id = 1, Street = "Доваторцев" },
                new StreetTable { Id = 2, Street = "Кабардинская" },
                new StreetTable { Id = 3, Street = "Кулакова" },
                new StreetTable { Id = 4, Street = "Суварова" },
                new StreetTable { Id = 5, Street = "Мальбохова" }
            );

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Faculty).HasColumnName("faculty");
            entity.Property(e => e.FullName).HasColumnName("fullName");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Postindex).HasColumnName("postindex");
            entity.Property(e => e.Speciality).HasColumnName("speciality");
            entity.Property(e => e.Street).HasColumnName("street");

            entity.HasOne(d => d.City).WithMany(p => p.Students)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("student_cityId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

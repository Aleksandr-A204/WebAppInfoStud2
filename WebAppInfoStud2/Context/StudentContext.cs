using System;
using System.Collections.Generic;
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

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<CourseTable> CourseTables { get; set; }

    public virtual DbSet<Curriculum> Curriculums { get; set; }

    public virtual DbSet<FacultyTable> FacultyTables { get; set; }

    public virtual DbSet<GroupTable> GroupTables { get; set; }

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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Contacts");

            entity.ToTable("contact");

            entity.HasIndex(e => e.Email, "contacts_Email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "contacts_Phone_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone).HasColumnName("phone");
        });

        modelBuilder.Entity<CourseTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("courseTable_pkey");

            entity.ToTable("courseTable");

            entity.HasIndex(e => e.Course, "courseTable_course_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Course).HasColumnName("course");
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("curriculum_pkey");

            entity.ToTable("curriculum");

            entity.HasIndex(e => new { e.FacultyId, e.SpecialityId, e.CourseId, e.GroupId }, "curriculum_facultyId_specialityId_courseId_groupId_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("courseId");
            entity.Property(e => e.FacultyId).HasColumnName("facultyId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.SpecialityId).HasColumnName("specialityId");

            entity.HasOne(d => d.Course).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("curriculum_courseId_fkey");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("curriculum_facultyId_fkey");

            entity.HasOne(d => d.Group).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("curriculum_groupId_fkey");

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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Faculty).HasColumnName("faculty");
        });

        modelBuilder.Entity<GroupTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("groupTable_pkey");

            entity.ToTable("groupTable");

            entity.HasIndex(e => e.Group, "groupTable_group_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Group).HasColumnName("group");
        });

        modelBuilder.Entity<PostindexTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("postindexTable_pkey");

            entity.ToTable("postindexTable");

            entity.HasIndex(e => e.PostIndex, "postindexTable_postIndex_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostIndex).HasColumnName("postIndex");
        });

        modelBuilder.Entity<SpecialityTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialityTable_pkey");

            entity.ToTable("specialityTable");

            entity.HasIndex(e => e.Speciality, "specialityTable_speciality_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Speciality).HasColumnName("speciality");
        });

        modelBuilder.Entity<StreetTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("streetTable_pkey");

            entity.ToTable("streetTable");

            entity.HasIndex(e => e.Street, "streetTable_street_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Faculty).HasColumnName("faculty");
            entity.Property(e => e.FullName).HasColumnName("fullName");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Postindex).HasColumnName("postindex");
            entity.Property(e => e.Speciality).HasColumnName("speciality");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

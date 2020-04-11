﻿using MCBLabber.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MCBLabber
{
    public class LabberDBContext : DbContext
    {
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Journal_Lab> Journals_Labs { get; set; }

        public LabberDBContext([NotNull] DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            if (Database.EnsureCreated())
            {
                Add(new Role("admin"));
                Add(new Role("teacher"));
                SaveChanges();
                Add(new User(Roles.FirstOrDefault().Id, "adminPOIT", "28032001"));
                Add(new User(Roles.FirstOrDefault(x => x.Title == "teacher").Id, "mvmenshikova"));
                SaveChanges();
                Add(new Teacher(Users.FirstOrDefault(x => x.Name == "mvmenshikova").Id, "Меньшикова", "Марина", "Валерьевна"));
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LabsModel(modelBuilder);
            MarksModel(modelBuilder);
            RolesModel(modelBuilder);
            UsersModel(modelBuilder);
            GroupsModel(modelBuilder);
            JournalsModel(modelBuilder);
            StudentsModel(modelBuilder);
            SubjectsModel(modelBuilder);
            TeachersModel(modelBuilder);
            Journals_LabsModel(modelBuilder);
        }

        private static void LabsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lab>(l =>
            {
                l.ToTable("labs");
                l.Property(x => x.Title).HasColumnType("varchar(15)");
            });
        }
        private static void MarksModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mark>(m =>
            {
                m.ToTable("marks");
                m.Property(x => x.PracticeState).HasColumnType("varchar(2)");
                m.Property(x => x.TheoryState).HasColumnType("varchar(2)");
                m.Property(x => x.Date).HasColumnType("varchar(10)");
                m.HasIndex(x => new { x.StudentId, x.Journal_LabId }).IsUnique();
                m.HasOne(x => x.Student).WithMany().HasForeignKey(x => x.StudentId);
                m.HasOne(x => x.Journal_Lab).WithMany().HasForeignKey(x => x.Journal_LabId);
            });
        }
        private static void RolesModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(r =>
            {
                r.ToTable("roles");
                r.Property(x => x.Title).HasColumnType("varchar(15)").IsRequired();
                r.HasIndex(x => x.Title).IsUnique();
            });
        }
        private static void UsersModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("users");
                u.Property(x => x.Name).HasColumnType("varchar(15)").IsRequired();
                u.HasIndex(x => x.Name).IsUnique();
                u.Property(x => x.Password).HasColumnType("varchar(15)").IsRequired();
                u.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).IsRequired();
            });
        }
        private static void GroupsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(g =>
            {
                g.ToTable("groups");
                g.Property(x => x.Title).HasColumnType("varchar(7)").IsRequired();
                g.HasIndex(x => x.Title).IsUnique();
            });
        }
        private static void JournalsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>(j =>
            {
                j.ToTable("journals");
                j.HasOne(x => x.Group).WithMany().HasForeignKey(x => x.GroupId);
                j.HasOne(x => x.Subject).WithMany().HasForeignKey(x => x.SubjectId);
                j.HasOne(x => x.Teacher).WithMany().HasForeignKey(x => x.TeacherId);
                j.HasIndex(x => new { x.GroupId, x.SubjectId, x.TeacherId, x.SubGroup }).IsUnique();
            });
        }
        private static void StudentsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(st =>
            {
                st.ToTable("students");
                st.Property(x => x.Surname).HasColumnType("varchar(15)").IsRequired();
                st.Property(x => x.FirstName).HasColumnType("varchar(15)").IsRequired();
                st.Property(x => x.SecondName).HasColumnType("varchar(15)").IsRequired();
                st.Property(x => x.SubGroup).HasColumnType("varchar(1)").IsRequired();
                st.HasIndex(x => new { x.Surname, x.FirstName, x.SecondName }).IsUnique();
                st.HasOne(x => x.Group).WithMany().HasForeignKey(x => x.GroupId).IsRequired();
            });
        }
        private static void SubjectsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>(su =>
            {
                su.ToTable("subjects");
                su.Property(x => x.ShortTitle).HasColumnType("varchar(15)").IsRequired();
                su.Property(x => x.LongTitle).HasColumnType("varchar(50)").IsRequired();
                su.HasIndex(x => new { x.ShortTitle, x.LongTitle }).IsUnique();
            });
        }
        private static void TeachersModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>(t =>
            {
                t.ToTable("teachers");
                t.HasKey(x => x.UserId);
                t.Property(x => x.Surname).HasColumnType("varchar(15)").IsRequired();
                t.Property(x => x.FirstName).HasColumnType("varchar(15)").IsRequired();
                t.Property(x => x.SecondName).HasColumnType("varchar(15)").IsRequired();
                t.HasIndex(x => new { x.Surname, x.FirstName, x.SecondName }).IsUnique();
                t.HasOne(x => x.User).WithOne().HasForeignKey<Teacher>(x => x.UserId).IsRequired();
            });
        }
        private static void Journals_LabsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal_Lab>(jl =>
            {
                jl.ToTable("journals_labs");
                jl.Property(x => x.Date).HasColumnType("varchar(10)");
                jl.HasIndex(x => new { x.JournalId, x.LabId }).IsUnique();
                jl.HasOne(x => x.Journal).WithMany().HasForeignKey(x => x.JournalId).IsRequired();
                jl.HasOne(x => x.Lab).WithMany().HasForeignKey(x => x.LabId).IsRequired();
            });
        }
    }
}

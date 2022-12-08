using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Employment.Data
{
    public partial class EmploymentContext : DbContext
    {
        public EmploymentContext()
        {
        }

        public EmploymentContext(DbContextOptions<EmploymentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<FullAddress> FullAddresses { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Locality> Localities { get; set; } = null!;
        public virtual DbSet<LocalityType> LocalityTypes { get; set; } = null!;
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Requirment> Requirments { get; set; } = null!;
        public virtual DbSet<Responsibility> Responsibilities { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<SocialResponsibility> SocialResponsibilities { get; set; } = null!;
        public virtual DbSet<Street> Streets { get; set; } = null!;
        public virtual DbSet<StreetType> StreetTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Employment;Trusted_Connection=True;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CorpusNumber).HasColumnName("corpus_number");

                entity.Property(e => e.FlatNumber).HasColumnName("flat_number");

                entity.Property(e => e.HouseNumber).HasColumnName("house_number");

                entity.Property(e => e.StreetId).HasColumnName("street_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Address__company__72C60C4A");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__street___71D1E811");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(30)
                    .HasColumnName("short_name");
            });

            modelBuilder.Entity<FullAddress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("FullAddress");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CorpusNumber).HasColumnName("corpus_number");

                entity.Property(e => e.FlatNumber).HasColumnName("flat_number");

                entity.Property(e => e.HouseNumber).HasColumnName("house_number");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LocalityName)
                    .HasMaxLength(50)
                    .HasColumnName("locality_name");

                entity.Property(e => e.ShortLocalityType)
                    .HasMaxLength(30)
                    .HasColumnName("short_locality_type");

                entity.Property(e => e.ShortStreetType)
                    .HasMaxLength(30)
                    .HasColumnName("short_street_type");

                entity.Property(e => e.StreetName)
                    .HasMaxLength(50)
                    .HasColumnName("street_name");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(3)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.ToTable("Locality");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LocalityTypeId).HasColumnName("locality_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.LocalityType)
                    .WithMany(p => p.Localities)
                    .HasForeignKey(d => d.LocalityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Locality__locali__6A30C649");
            });

            modelBuilder.Entity<LocalityType>(entity =>
            {
                entity.ToTable("LocalityType");

                entity.HasIndex(e => e.ShortName, "UQ__Locality__2711634FD121EFCA")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__Locality__72E12F1B9ED0550B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(30)
                    .HasColumnName("short_name");
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.ToTable("PhoneNumber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.PhoneNumber1)
                    .HasMaxLength(16)
                    .HasColumnName("phone_number");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__PhoneNumb__compa__75A278F5");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("begin_date");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(30)
                    .HasColumnName("short_name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Post__company_id__7A672E12");
            });

            modelBuilder.Entity<Requirment>(entity =>
            {
                entity.ToTable("Requirment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommunicationSkill)
                    .HasMaxLength(3)
                    .HasColumnName("communication_skill");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.LowerAgeLimit).HasColumnName("lower_age_limit");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UpperAgeLimit).HasColumnName("upper_age_limit");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Requirments)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Requirmen__gende__0C85DE4D");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Requirments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Requirmen__post___0B91BA14");
            });

            modelBuilder.Entity<Responsibility>(entity =>
            {
                entity.ToTable("Responsibility");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Responsibilities)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Responsib__post___7D439ABD");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LowerLimit)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("lower_limit");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UpperLimit)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("upper_limit");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Salary__post_id__04E4BC85");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("Skill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Level)
                    .HasMaxLength(80)
                    .HasColumnName("level");

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .HasColumnName("name");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Skill__post_id__0F624AF8");
            });

            modelBuilder.Entity<SocialResponsibility>(entity =>
            {
                entity.ToTable("SocialResponsibility");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmploymentBook)
                    .HasMaxLength(50)
                    .HasColumnName("employment_book");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.SocialPackage)
                    .HasMaxLength(50)
                    .HasColumnName("social_package");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.SocialResponsibilities)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__SocialRes__post___00200768");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("Street");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LocalityId).HasColumnName("locality_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.StreetTypeId).HasColumnName("street_type_id");

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.LocalityId)
                    .HasConstraintName("FK__Street__locality__6EF57B66");

                entity.HasOne(d => d.StreetType)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.StreetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Street__street_t__6E01572D");
            });

            modelBuilder.Entity<StreetType>(entity =>
            {
                entity.ToTable("StreetType");

                entity.HasIndex(e => e.ShortName, "UQ__StreetTy__2711634F3749F88C")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__StreetTy__72E12F1BA0E13E8A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(30)
                    .HasColumnName("short_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

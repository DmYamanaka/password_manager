using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models.User;
using PaswordManager.Models;

namespace PaswordManager.Context;

public partial class PasswordManagerRepasitory : DbContext
{
    public PasswordManagerRepasitory()
    {
    }

    public PasswordManagerRepasitory(DbContextOptions<PasswordManagerRepasitory> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Password> Passwords { get; set; }

    public virtual DbSet<PasswordHistory> PasswordHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPasswordHistory> UserPasswordHistories { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-RKKPMUF;Database=Password_manager;Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Cards", "guest");

            entity.HasIndex(e => e.Id, "Cards_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("Delete_date");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("Edit_date");
            entity.Property(e => e.IdLogin).HasColumnName("Id_login");
            entity.Property(e => e.IdPassword).HasColumnName("Id_password");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.LinkResource).HasColumnName("Link_resource");

            entity.HasOne(d => d.IdLoginNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Id_login");

            entity.HasOne(d => d.IdPasswordNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdPassword)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Id_password");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Id_user");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.ToTable("Folders", "guest");

            entity.HasIndex(e => e.Id, "Folders_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("Delete_date");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("Edit_date");
            entity.Property(e => e.IdCard).HasColumnName("Id_card");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Folders)
                .HasForeignKey(d => d.IdCard)
                .HasConstraintName("Id_card");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("Logins_pk")
                .IsClustered(false);

            entity.ToTable("Logins", "guest");

            entity.HasIndex(e => e.Id, "Logins_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("Delete_date");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("Edit_date");
            entity.Property(e => e.Login1).HasColumnName("Login");
        });

        modelBuilder.Entity<Password>(entity =>
        {
            entity.ToTable("Passwords", "guest");

            entity.HasIndex(e => e.Id, "Passwords_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("Delete_date");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("Edit_date");
        });

        modelBuilder.Entity<PasswordHistory>(entity =>
        {
            entity.ToTable("Password_history", "guest");

            entity.HasIndex(e => e.Id, "Password_history_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.IdPassword).HasColumnName("Id_password");

            entity.HasOne(d => d.IdPasswordNavigation).WithMany(p => p.PasswordHistories)
                .HasForeignKey(d => d.IdPassword)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Id_passwords");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "guest");

            entity.HasIndex(e => e.Id, "Users_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("Delete_date");
            entity.Property(e => e.EMail).HasColumnName("E-mail");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("Edit_date");
        });

        modelBuilder.Entity<UserPasswordHistory>(entity =>
        {
            entity.ToTable("User_password_history", "guest");

            entity.HasIndex(e => e.Id, "User_password_history_Id_uindex").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Create_date");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPasswordHistories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Id_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.Models;

public partial class DentalSystemDbContext : DbContext
{
    public DentalSystemDbContext()
    {
    }

    public DentalSystemDbContext(DbContextOptions<DentalSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialExport> MaterialExports { get; set; }

    public virtual DbSet<MaterialImport> MaterialImports { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientRecord> PatientRecords { get; set; }

    public virtual DbSet<PatientRecordServiceMap> PatientRecordServiceMaps { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermissionMap> RolePermissionMaps { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<Timekeeping> Timekeepings { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

    public virtual DbSet<TreatmentServiceMap> TreatmentServiceMaps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =DESKTOP-3PS4NQ2; database = DentalSystemDB;uid=sa;pwd=dopi4720;encrypt=true;trustServerCertificate=true;");

        => optionsBuilder.UseSqlServer("server =MSI\\SQLEXPRESS; database = DentalSystemDB;uid=sa;pwd=sa;encrypt=true;trustServerCertificate=true;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("materials");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Unit)
                .IsUnicode(false)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<MaterialExport>(entity =>
        {
            entity.ToTable("material_export");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.PatientRecordId).HasColumnName("patient_record_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialExports)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_material_export_materials");

            entity.HasOne(d => d.PatientRecord).WithMany(p => p.MaterialExports)
                .HasForeignKey(d => d.PatientRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_material_export_patient_record");
        });

        modelBuilder.Entity<MaterialImport>(entity =>
        {
            entity.ToTable("material_import");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.SupplyName)
                .IsUnicode(false)
                .HasColumnName("supply_name");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialImports)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_material_import_materials");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patients__3213E83F28CB1AF0");

            entity.ToTable("patients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.BodyPrehistory).HasColumnName("body_prehistory");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TeethPrehistory).HasColumnName("teeth_prehistory");
        });

        modelBuilder.Entity<PatientRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patient___3213E83F5D17537E");

            entity.ToTable("patient_record");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Causal).HasColumnName("causal");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Debit).HasColumnName("debit");
            entity.Property(e => e.Diagnostic).HasColumnName("diagnostic");
            entity.Property(e => e.MarrowRecord).HasColumnName("marrow_record");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Prescription).HasColumnName("prescription");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.TreatmentId).HasColumnName("treatment_id");
            entity.Property(e => e.TreatmentName).HasColumnName("treatment_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patient_record_patients");

            entity.HasOne(d => d.User).WithMany(p => p.PatientRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patient_record_users");
        });

        modelBuilder.Entity<PatientRecordServiceMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patient___3213E83F4BB427C9");

            entity.ToTable("patient_record_service_map");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PatientRecordId).HasColumnName("patient_record_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.PatientRecord).WithMany(p => p.PatientRecordServiceMaps)
                .HasForeignKey(d => d.PatientRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patient_record_service_map_patient_record");

            entity.HasOne(d => d.Service).WithMany(p => p.PatientRecordServiceMaps)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patient_record_service_map_services");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83FC1B58832");

            entity.ToTable("permission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F93002943");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<RolePermissionMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role_per__3213E83FD2ADDCFF");

            entity.ToTable("role_permission_map");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissionMaps)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_permission_map_permission");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionMaps)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_permission_map_roles");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F1A5E96AE");

            entity.ToTable("schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Booked).HasColumnName("booked");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Patient).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_schedule_patients");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83FFD09176D");

            entity.ToTable("services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MarketPrice).HasColumnName("market_price");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemLo__3214EC075467928E");

            entity.ToTable("SystemLog");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Owner).WithMany(p => p.SystemLogs)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SystemLog__Owner__6B24EA82");
        });

        modelBuilder.Entity<Timekeeping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__timekeep__3213E83F3593246D");

            entity.ToTable("timekeeping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TimeCheckin)
                .HasColumnType("datetime")
                .HasColumnName("time_checkin");
            entity.Property(e => e.TimeCheckout)
                .HasColumnType("datetime")
                .HasColumnName("time_checkout");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Timekeepings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_timekeeping_users");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__treatmen__3213E83FBD26C1B6");

            entity.ToTable("treatments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_treatments_patients");
        });

        modelBuilder.Entity<TreatmentServiceMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__treatmen__3213E83F4C52954D");

            entity.ToTable("treatment_service_map");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentPrice).HasColumnName("current_price");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.PatientRecordId).HasColumnName("patient_record_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TreatmentId).HasColumnName("treatment_id");

            entity.HasOne(d => d.PatientRecord).WithMany(p => p.TreatmentServiceMaps)
                .HasForeignKey(d => d.PatientRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_treatment_service_map_patient_record");

            entity.HasOne(d => d.Service).WithMany(p => p.TreatmentServiceMaps)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_treatment_service_map_services");

            entity.HasOne(d => d.Treatment).WithMany(p => p.TreatmentServiceMaps)
                .HasForeignKey(d => d.TreatmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_treatment_service_map_treatments");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FB32E9154");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Enable).HasColumnName("enable");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

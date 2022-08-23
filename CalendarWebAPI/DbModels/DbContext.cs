using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CalendarWebAPI.DbModels
{
    public partial class CalendarContext : DbContext
    {
        public CalendarContext()
        {
        }

        public CalendarContext(DbContextOptions<CalendarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calendar> Calendars { get; set; } = null!;
        public virtual DbSet<CalendarDate> CalendarDates { get; set; } = null!;
        public virtual DbSet<CalendarItem> CalendarItems { get; set; } = null!;
        public virtual DbSet<Confession> Confessions { get; set; } = null!;
        public virtual DbSet<Creator> Creators { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Holiday> Holidays { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonalIncome> PersonalIncomes { get; set; } = null!;
        public virtual DbSet<Recurring> Recurrings { get; set; } = null!;
        public virtual DbSet<Scheduler> Schedulers { get; set; } = null!;
        public virtual DbSet<SchedulerItem> SchedulerItems { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;
        public virtual DbSet<TaxGroup> TaxGroups { get; set; } = null!;
        public virtual DbSet<TaxInTaxGroup> TaxInTaxGroups { get; set; } = null!;
        public virtual DbSet<Taxis> Taxes { get; set; } = null!;
        public virtual DbSet<WorkingDay> WorkingDays { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Calendar;User ID=sa;Password=Andelo1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("Calendar", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IsApproved).HasDefaultValueSql("((1))");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Calendars)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK_Calendar_Creators");

                entity.HasOne(d => d.Paent)
                    .WithMany(p => p.InversePaent)
                    .HasForeignKey(d => d.PaentId)
                    .HasConstraintName("FK_Calendar_Calendar");
            });

            modelBuilder.Entity<CalendarDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CalendarDate", "Catalog");

                entity.Property(e => e.MMYYYY)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Style101)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Style103)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Style112)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Style120)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TheDate).HasColumnType("date");

                entity.Property(e => e.TheDayName).HasMaxLength(30);

                entity.Property(e => e.TheDaySuffix)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TheFirstOfMonth).HasColumnType("date");

                entity.Property(e => e.TheFirstOfNextMonth).HasColumnType("date");

                entity.Property(e => e.TheFirstOfQuarter).HasColumnType("date");

                entity.Property(e => e.TheFirstOfWeek).HasColumnType("date");

                entity.Property(e => e.TheFirstOfYear).HasColumnType("date");

                entity.Property(e => e.TheLastOfMonth).HasColumnType("date");

                entity.Property(e => e.TheLastOfNextMonth).HasColumnType("date");

                entity.Property(e => e.TheLastOfQuarter).HasColumnType("date");

                entity.Property(e => e.TheLastOfWeek).HasColumnType("date");

                entity.Property(e => e.TheLastOfYear).HasColumnType("date");

                entity.Property(e => e.TheMonthName).HasMaxLength(30);
            });

            modelBuilder.Entity<CalendarItem>(entity =>
            {
                entity.ToTable("CalendarItems", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.GivenName).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsApproved).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsHoliday).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMemorialday).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsWeekendday).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsWorkingday).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.CalendarItems)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarItems_Calendar");
            });

            modelBuilder.Entity<Confession>(entity =>
            {
                entity.ToTable("Confessions", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Creator>(entity =>
            {
                entity.ToTable("Creators", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Events", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Coefficient)
                    .HasColumnType("decimal(5, 2)")
                    .HasDefaultValueSql("((1.0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Redovan rad')");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Reccuring)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ReccuringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Recurrings");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holidays", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateDay)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.DateMonth)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.IsCommon).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPermanent).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Confession)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.ConfessionId)
                    .HasConstraintName("FK_Holidays_Confessions");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("People", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Adress).HasMaxLength(250);

                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Country).HasMaxLength(250);

                entity.Property(e => e.CountryOfResidence).HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.ImageUrl).HasMaxLength(250);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.PersonalIdentificationNumber).HasMaxLength(250);

                entity.Property(e => e.PostalCode).HasMaxLength(30);

                entity.Property(e => e.RecordDtModified).HasColumnType("date");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PersonalIncome>(entity =>
            {
                entity.ToTable("PersonalIncomes", "Person");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Recurring>(entity =>
            {
                entity.ToTable("Recurrings", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ReccuringType).HasMaxLength(50);
            });

            modelBuilder.Entity<Scheduler>(entity =>
            {
                entity.ToTable("Scheduler", "Person");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Schedulers)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scheduler_Events");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Schedulers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scheduler_People");
            });

            modelBuilder.Entity<SchedulerItem>(entity =>
            {
                entity.ToTable("SchedulerItems", "Person");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.CalendarItems)
                    .WithMany(p => p.SchedulerItems)
                    .HasForeignKey(d => d.CalendarItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchedulerItems_CalendarItems");

                entity.HasOne(d => d.Scheduler)
                    .WithMany(p => p.SchedulerItems)
                    .HasForeignKey(d => d.SchedulerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchedulerItems_Scheduler");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.ToTable("Shifts", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.EndTime).HasColumnType("smalldatetime");

                entity.Property(e => e.StartTime).HasColumnType("smalldatetime");

                entity.HasOne(d => d.CalendarItem)
                    .WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.CalendarItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shifts_CalendarItems");
            });

            modelBuilder.Entity<TaxGroup>(entity =>
            {
                entity.ToTable("TaxGroups", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("numeric(12, 6)");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Percent).HasColumnType("decimal(8, 6)");
            });

            modelBuilder.Entity<TaxInTaxGroup>(entity =>
            {
                entity.ToTable("TaxInTaxGroups", "Catalog");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.TaxGroup)
                    .WithMany(p => p.TaxInTaxGroups)
                    .HasForeignKey(d => d.TaxGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxInTaxGroups_ToTaxGroups");

                entity.HasOne(d => d.Tax)
                    .WithMany(p => p.TaxInTaxGroups)
                    .HasForeignKey(d => d.TaxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxInTaxGroups_ToTaxes");
            });

            modelBuilder.Entity<Taxis>(entity =>
            {
                entity.ToTable("Taxes", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasColumnType("numeric(12, 6)");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.DateEnd).HasMaxLength(20);

                entity.Property(e => e.DateStart).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Percent).HasColumnType("decimal(8, 6)");
            });

            modelBuilder.Entity<WorkingDay>(entity =>
            {
                entity.ToTable("WorkingDays", "Catalog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Friday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Monday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Thursday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Tuseday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Wednesday)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

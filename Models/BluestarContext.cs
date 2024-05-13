using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Models;

public partial class BluestarContext : DbContext
{
    public BluestarContext()
    {
    }

    public BluestarContext(DbContextOptions<BluestarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Chuyenbay> Chuyenbays { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Luggage> Luggage { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Sanbay> Sanbays { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public DbSet<Seat> Seats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BLUESTAR;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AS_SC_UTF8");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__ACCOUNT__161CF725E0FECA79");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("POSITION");
        });

        modelBuilder.Entity<Chuyenbay>(entity =>
        {
            entity.HasKey(e => e.FlyId).HasName("PK__CHUYENBA__337DF9F42EAD0A88");

            entity.ToTable("CHUYENBAY");

            entity.Property(e => e.FlyId)
                .HasMaxLength(20)
                .HasColumnName("flyID");
            entity.Property(e => e.AirportId)
                .HasMaxLength(5)
                .HasColumnName("airport_ID");
            entity.Property(e => e.ArrivalTime)
                .HasMaxLength(5)
                .HasColumnName("arrivalTime");
            entity.Property(e => e.DepartureDay)
                .HasMaxLength(10)
                .HasColumnName("departureDay");
            entity.Property(e => e.DepartureTime)
                .HasMaxLength(5)
                .HasColumnName("departureTime");
            entity.Property(e => e.FromLocation)
                .HasMaxLength(255)
                .HasColumnName("fromLocation");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("int")
                .HasColumnName("originalPrice");
            entity.Property(e => e.PlId)
                .HasMaxLength(5)
                .HasColumnName("Pl_ID");
            entity.Property(e => e.ToLocation)
                .HasMaxLength(255)
                .HasColumnName("toLocation");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__CUSTOMER__A9FDEC12AFB11635");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("C_ID");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Mail)
                .HasMaxLength(30)
                .HasColumnName("MAIL");
            entity.Property(e => e.NumId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUM_ID");
            entity.Property(e => e.Point)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("POINT");

        //    entity.HasOne(d => d.MailNavigation).WithMany(p => p.Customers)
        //        .HasForeignKey(d => d.Mail)
        //       .HasConstraintName("fk_customer");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DId).HasName("pk_discount");

            entity.ToTable("DISCOUNT");

            entity.Property(e => e.DId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("D_ID");
            entity.Property(e => e.DFinish)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("D_FINISH");
            entity.Property(e => e.DName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("D_NAME");
            entity.Property(e => e.DPercent).HasColumnName("D_PERCENT");
            entity.Property(e => e.DStart)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("D_START");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FId).HasName("pk_food");

            entity.ToTable("FOOD");

            entity.Property(e => e.FId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("F_ID");
            entity.Property(e => e.FName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("F_NAME");
            entity.Property(e => e.FPrice)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("F_PRICE");
        });

        modelBuilder.Entity<Luggage>(entity =>
        {
            entity.HasKey(e => e.LuggageCode).HasName("PK__LUGGAGE__1C0F361D9705AAFB");

            entity.ToTable("LUGGAGE");

            entity.Property(e => e.LuggageCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LUGGAGE_CODE");
            entity.Property(e => e.Mass)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("MASS");
            entity.Property(e => e.Price).HasColumnName("PRICE");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.PlId).HasName("pk_plane");

            entity.ToTable("PLANE");

            entity.Property(e => e.PlId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("PL_ID");
            entity.Property(e => e.BusinessCapacity).HasColumnName("BUSINESS_CAPACITY");
            entity.Property(e => e.EconomyCapacity).HasColumnName("ECONOMY_CAPACITY");
            entity.Property(e => e.Typeofplane)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TYPEOFPLANE");
        });

        modelBuilder.Entity<Sanbay>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__SANBAY__C85BDF9E80C3A36E");

            entity.ToTable("SANBAY");

            entity.Property(e => e.AirportId)
                .HasMaxLength(20)
                .HasColumnName("airportID");
            entity.Property(e => e.AirportName)
                .HasMaxLength(255)
                .HasColumnName("airportName");
            entity.Property(e => e.Place)
                .HasMaxLength(255)
                .HasColumnName("place");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TId).HasName("PK__TICKET__83BB1FB2BDECFB45");

            entity.ToTable("TICKET");

            entity.HasIndex(e => new { e.FlyId, e.SeatId }, "UQ_Fly_Seat").IsUnique();

            entity.HasIndex(e => new { e.FlyId, e.SeatId, e.Cccd, e.Name, e.Mail }, "UQ_Fly_Seat_CCCD_Name_Mail").IsUnique();

            entity.Property(e => e.TId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("T_ID");
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.DisId)
                .HasMaxLength(5)
                .HasColumnName("Dis_ID");
            entity.Property(e => e.FlyId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Fly_ID");
            entity.Property(e => e.FoodId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Food_ID");
            entity.Property(e => e.KgId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Kg_ID");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SeatId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Seat_ID");
            entity.Property(e => e.TicketPrice)
            .HasColumnName("Ticket_Price")
            .HasColumnType("bigint");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__SEAT__79B89923D3A743A3");

            entity.ToTable("SEAT");

            entity.Property(e => e.SeatId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_ID");
            entity.Property(e => e.FlightId)
                .HasMaxLength(20)
                .HasColumnName("FLIGHT_ID");
            entity.Property(e => e.IsBooked).HasColumnName("ISBOOKED");
            entity.Property(e => e.SeatType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SEAT_TYPE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

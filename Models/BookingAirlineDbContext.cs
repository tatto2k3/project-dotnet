using System;
using System.Collections.Generic;
using BlueStarMVC.EntityFramwork.Models;
using System.Data;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.EntityFramwork.Models;

public partial class BookingAirlineDbContext : DbContext
{
    public BookingAirlineDbContext()
    {
    }

    public BookingAirlineDbContext(DbContextOptions<BookingAirlineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddionalFoodService> AddionalFoodServices { get; set; }

    public virtual DbSet<AdditionalSeatService> AdditionalSeatServices { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FlightDetail> FlightDetails { get; set; }

    public virtual DbSet<FoodForFlight> FoodForFlights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationMapAddionalFoodService> ReservationMapAddionalFoodServices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SeatDetail> SeatDetails { get; set; }

    public virtual DbSet<ServiceForClass> ServiceForClasses { get; set; }

    public virtual DbSet<TicketPrice> TicketPrices { get; set; }

    public virtual DbSet<TokenRemainLogin> TokenRemainLogins { get; set; }

    public virtual DbSet<TravelClass> TravelClasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ADMIN-PC;Database=BookingAirlineDB;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddionalFoodService>(entity =>
        {
            entity.ToTable("AddionalFoodService");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FoodPrice).HasColumnType("decimal(18, 2)");

            entity.HasMany(d => d.FlightDetails).WithMany(p => p.AddionalFoodServices)
                .UsingEntity<Dictionary<string, object>>(
                    "AddionalFoodServiceFlightDetail",
                    r => r.HasOne<FlightDetail>().WithMany().HasForeignKey("FlightDetailsId"),
                    l => l.HasOne<AddionalFoodService>().WithMany().HasForeignKey("AddionalFoodServicesId"),
                    j =>
                    {
                        j.HasKey("AddionalFoodServicesId", "FlightDetailsId");
                        j.ToTable("AddionalFoodServiceFlightDetail");
                        j.HasIndex(new[] { "FlightDetailsId" }, "IX_AddionalFoodServiceFlightDetail_flightDetailsId");
                        j.IndexerProperty<Guid>("FlightDetailsId").HasColumnName("flightDetailsId");
                    });

            entity.HasMany(d => d.ReservationDetails).WithMany(p => p.AddionalFoodServices)
                .UsingEntity<Dictionary<string, object>>(
                    "AddionalFoodServiceReservation",
                    r => r.HasOne<Reservation>().WithMany().HasForeignKey("ReservationDetailsId"),
                    l => l.HasOne<AddionalFoodService>().WithMany().HasForeignKey("AddionalFoodServicesId"),
                    j =>
                    {
                        j.HasKey("AddionalFoodServicesId", "ReservationDetailsId");
                        j.ToTable("AddionalFoodServiceReservation");
                        j.HasIndex(new[] { "ReservationDetailsId" }, "IX_AddionalFoodServiceReservation_reservationDetailsId");
                        j.IndexerProperty<Guid>("ReservationDetailsId").HasColumnName("reservationDetailsId");
                    });
        });

        modelBuilder.Entity<AdditionalSeatService>(entity =>
        {
            entity.ToTable("AdditionalSeatService");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SeatLevel).HasColumnName("seatLevel");
            entity.Property(e => e.SeatPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("seatPrice");
            entity.Property(e => e.SeatType).HasColumnName("seatType");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasIndex(e => e.CountryId, "IX_Airports_CountryId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Country).WithMany(p => p.Airports).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ContryName).HasColumnName("contryName");
            entity.Property(e => e.CountryCode).HasColumnName("countryCode");
        });

        modelBuilder.Entity<FlightDetail>(entity =>
        {
            entity.HasIndex(e => e.DestinationAirportId, "IX_FlightDetails_DestinationAirportId");

            entity.HasIndex(e => e.SourceAirportId, "IX_FlightDetails_SourceAirportId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DepartureDate).HasColumnType("datetime");

            entity.HasOne(d => d.DestinationAirport).WithMany(p => p.FlightDetailDestinationAirports)
                .HasForeignKey(d => d.DestinationAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SourceAirport).WithMany(p => p.FlightDetailSourceAirports)
                .HasForeignKey(d => d.SourceAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FoodForFlight>(entity =>
        {
            entity.HasKey(e => new { e.FlightId, e.FoodId });

            entity.ToTable("FoodForFlight");

            entity.HasIndex(e => e.FlightDetailId, "IX_FoodForFlight_FlightDetailId");

            entity.HasIndex(e => e.FoodServiceId, "IX_FoodForFlight_FoodServiceId");

            entity.HasOne(d => d.FlightDetail).WithMany(p => p.FoodForFlights).HasForeignKey(d => d.FlightDetailId);

            entity.HasOne(d => d.FoodService).WithMany(p => p.FoodForFlights).HasForeignKey(d => d.FoodServiceId);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.ReservationIdid, "IX_Payments_ReservationIDId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReservationIdid).HasColumnName("ReservationIDId");

            entity.HasOne(d => d.ReservationId).WithMany(p => p.Payments).HasForeignKey(d => d.ReservationIdid);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_RefreshTokens_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasIndex(e => e.PassengerIdid, "IX_Reservations_PassengerIDId");

            entity.HasIndex(e => e.SeatDetailsIdid, "IX_Reservations_SeatDetailsIDId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PassengerIdid).HasColumnName("PassengerIDId");
            entity.Property(e => e.SeatDetailsIdid).HasColumnName("SeatDetailsIDId");

            entity.HasOne(d => d.PassengerId).WithMany(p => p.Reservations).HasForeignKey(d => d.PassengerIdid);

            entity.HasOne(d => d.SeatDetailsId).WithMany(p => p.Reservations).HasForeignKey(d => d.SeatDetailsIdid);
        });

        modelBuilder.Entity<ReservationMapAddionalFoodService>(entity =>
        {
            entity.HasKey(e => new { e.ReservationId, e.AdditionalFoodServiceId });

            entity.ToTable("ReservationMapAddionalFoodService");

            entity.HasIndex(e => e.AdditionalFoodServiceId, "IX_ReservationMapAddionalFoodService_AdditionalFoodServiceId");

            entity.HasOne(d => d.AdditionalFoodService).WithMany(p => p.ReservationMapAddionalFoodServices).HasForeignKey(d => d.AdditionalFoodServiceId);

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationMapAddionalFoodServices).HasForeignKey(d => d.ReservationId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleModelUser",
                    r => r.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                    l => l.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                    j =>
                    {
                        j.HasKey("RolesId", "UsersId");
                        j.ToTable("RoleModelUser");
                        j.HasIndex(new[] { "UsersId" }, "IX_RoleModelUser_UsersId");
                    });
        });

        modelBuilder.Entity<SeatDetail>(entity =>
        {
            entity.HasIndex(e => e.ClassId, "IX_SeatDetails_ClassId");

            entity.HasIndex(e => e.FlightId, "IX_SeatDetails_FlightId");

            entity.HasIndex(e => e.SeatAdditionalServiceId, "IX_SeatDetails_SeatAdditionalServiceId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Class).WithMany(p => p.SeatDetails).HasForeignKey(d => d.ClassId);

            entity.HasOne(d => d.Flight).WithMany(p => p.SeatDetails).HasForeignKey(d => d.FlightId);

            entity.HasOne(d => d.SeatAdditionalService).WithMany(p => p.SeatDetails).HasForeignKey(d => d.SeatAdditionalServiceId);
        });

        modelBuilder.Entity<ServiceForClass>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.TravelClasses).WithMany(p => p.ServiceForClasses)
                .UsingEntity<Dictionary<string, object>>(
                    "ServiceForClassTravelClass",
                    r => r.HasOne<TravelClass>().WithMany().HasForeignKey("TravelClassesId"),
                    l => l.HasOne<ServiceForClass>().WithMany().HasForeignKey("ServiceForClassesId"),
                    j =>
                    {
                        j.HasKey("ServiceForClassesId", "TravelClassesId");
                        j.ToTable("ServiceForClassTravelClass");
                        j.HasIndex(new[] { "TravelClassesId" }, "IX_ServiceForClassTravelClass_TravelClassesId");
                    });
        });

        modelBuilder.Entity<TicketPrice>(entity =>
        {
            entity.ToTable("TicketPrice");

            entity.HasIndex(e => e.ClassId, "IX_TicketPrice_ClassID");

            entity.HasIndex(e => e.FlightId, "IX_TicketPrice_FlightId");

            entity.Property(e => e.TicketPriceId).ValueGeneratedNever();
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Class).WithMany(p => p.TicketPrices).HasForeignKey(d => d.ClassId);

            entity.HasOne(d => d.Flight).WithMany(p => p.TicketPrices).HasForeignKey(d => d.FlightId);
        });

        modelBuilder.Entity<TokenRemainLogin>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_TokenRemainLogins_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.TokenRemainLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TravelClass>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
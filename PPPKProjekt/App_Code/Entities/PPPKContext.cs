namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PPPKContext : DbContext
    {
        public PPPKContext()
            : base("name=PPPKContext")
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceItem> ServiceItem { get; set; }
        public virtual DbSet<TravelOrder> TravelOrder { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleFuel> VehicleFuel { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bills>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceItem)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceItem>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TravelOrder>()
                .Property(e => e.Total_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TravelOrder>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.TravelOrder1)
                .HasForeignKey(e => e.TravelOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.TravelOrder)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.VehicleFuel)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Service)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VehicleFuel>()
                .Property(e => e.FuelPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<VehicleType>()
                .HasMany(e => e.Vehicle)
                .WithRequired(e => e.VehicleType)
                .WillCascadeOnDelete(false);
        }
    }
}

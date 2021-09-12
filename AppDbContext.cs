using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ef_core_example
{
    // public class AppDbContext : DbContextWithTriggers
    public class AppDbContext : DbContext
    {

        private const string Guid_Column_Type = "varchar(36)";
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
        }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Depot> Depots { get; set; }

        // public virtual DbSet<Listing> Listings { get; set; }

        // public virtual DbSet<Transaction> Transactions { get; set; }

        // public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        // public virtual DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>(profile =>
            {
                profile.ToTable("profile")
                        .HasKey(profile => profile.Id);

                profile.Property(p => p.Id)
                        .HasColumnType(Guid_Column_Type)
                        .IsRequired();

                profile.Property(p => p.Name)
                        .HasMaxLength(Profile.Max_Profile_Name_Length)
                        .IsRequired();
            });

            modelBuilder.Entity<Depot>(depot =>
            {
                depot.ToTable("depot")
                            .HasKey(depot => depot.Id);

                depot.HasOne(d => d.Profile)
                        .WithMany();
                // .IsRequired();

                depot.Property(d => d.Id)
                        .HasColumnType(Guid_Column_Type)
                        .IsRequired();

                depot.Property(d => d.DisplayName)
                        .HasMaxLength(Depot.Max_DisplayName_Length)
                        .IsRequired();

                depot.Property(d => d.ContactName)
                        .HasMaxLength(Depot.Max_ContactName_Length)
                        .IsRequired();

                depot.Property(d => d.ContactPhone)
                        .HasMaxLength(Depot.Max_ContactPhone_Length)
                        .IsRequired();

                depot.Property(d => d.DepotId)
                        .HasMaxLength(Depot.Max_DepotId_Length)
                        .IsRequired();

                depot.Property(d => d.ContactEmail)
                        .HasConversion(p => p.Value, p => Email.Create(p).Value)
                        .HasMaxLength(Email.Max_Email_Length);

                depot.OwnsOne(d => d.DeliveryAddress, a =>
                {
                    a.Property(aa => aa.Address1);
                    a.Property(aa => aa.Address2);
                    a.Property(aa => aa.Address3);
                    a.Property(aa => aa.Address4);
                    a.Property(aa => aa.PostCode);
                });

                depot.OwnsOne(d => d.BillingAddress, a =>
                {
                    a.Property(aa => aa.Address1);
                    a.Property(aa => aa.Address2);
                    a.Property(aa => aa.Address3);
                    a.Property(aa => aa.Address4);
                    a.Property(aa => aa.PostCode);
                });

            });


            // modelBuilder.Entity<Depot>().ToTable("depot");
            // modelBuilder.Entity<Listing>().ToTable("listing");
            // modelBuilder.Entity<Transaction>().ToTable("transaction");
            // modelBuilder.Entity<Manufacturer>().ToTable("manufacturer");
            // modelBuilder.Entity<History>().ToTable("history");
        }
    }

    // public class History
    // {
    //     public int Id { get; set; }

    //     [Column(TypeName = "varchar(36)")]
    //     public Guid EntityId { get; set; }

    //     [MaxLength(56)]
    //     public string Entity { get; set; }

    //     public string Data { get; set; }

    //     public OperationType Operation { get; set; }

    //     public DateTime CreatedOn { get; set; }
    // }

    // public class MarketplaceModel : Trackable
    public class MarketplaceModel : Entity<Guid>
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public override Guid Id { get; protected set; }
    }

    // public abstract class Trackable
    // {

    //     static Trackable()
    //     {
    //         Triggers<Trackable, AppDbContext>.Inserted += entry => 
    //         {
    //             History history = ToHistory(entry, OperationType.Insert);

    //             entry.Context.History.Add(history);
    //             entry.Context.SaveChanges();
    //         };
    //         Triggers<Trackable, AppDbContext>.Updated += entry =>
    //         {
    //             History history = ToHistory(entry, OperationType.Update);

    //             entry.Context.History.Add(history);
    //             entry.Context.SaveChanges();
    //         };
    //         Triggers<Trackable, AppDbContext>.Deleted += entry =>
    //         {
    //             History history = ToHistory(entry, OperationType.Delete);

    //             entry.Context.History.Add(history);
    //             entry.Context.SaveChanges();
    //         };

    //         History ToHistory(IAfterEntry entry, OperationType type)
    //         {
    //             var entity = entry.Entity as MarketplaceModel;

    //             return new History()
    //             { 
    //                 Operation   = type, 
    //                 Entity      = nameof(entry.Entity),
    //                 EntityId    = entity.Id,
    //                 Data        = JsonConvert.SerializeObject(entry.Entity),
    //                 CreatedOn   = DateTime.Now
    //             };
    //         }
    //     }
    // }

    // public enum OperationType
    // {
    //     Insert = 1,
    //     Update = 5,
    //     Delete = 9
    // }
}
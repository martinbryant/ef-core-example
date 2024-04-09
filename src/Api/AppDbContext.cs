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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order =>
            {
                order.ToTable("order")
                        .HasKey(order => new { order.Id, order.Status });

                order.Property(o => o.Id)
                        .HasColumnType(Guid_Column_Type)
                        .IsRequired();
                order.Property(o => o.Status)
                        .IsRequired();
                order.Property(o => o.ActionedOn)
                        .IsRequired();
            });

            modelBuilder.Entity<Order>().HasData(
                    new Order() { Id = Guid.Parse("0155c260-e10e-4be5-19c3-08d98c18362b"), Status = OrderStatus.Reserved, ActionedOn = DateTime.Now },
                    new Order() { Id = Guid.Parse("0155c260-e10e-4be5-19c3-08d98c18362b"), Status = OrderStatus.Confirmed, ActionedOn = DateTime.Now },
                    new Order() { Id = Guid.Parse("6558ab54-d78a-404b-754d-08d9747e07f8"), Status = OrderStatus.Reserved, ActionedOn = DateTime.Now },
                    new Order() { Id = Guid.Parse("a0669672-483b-4fbd-958b-20d5cccfdc03"), Status = OrderStatus.Reserved, ActionedOn = DateTime.MinValue }
            );

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
    public class MarketplaceModel
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public Guid Id { get; set; }
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
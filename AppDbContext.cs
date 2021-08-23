using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ef_core_example.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ef_core_example
{
    public class AppDbContext : DbContextWithTriggers
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Depot> Depots { get; set; }

        public virtual DbSet<Listing> Listings { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("profile");
            modelBuilder.Entity<Depot>().ToTable("depot");
            modelBuilder.Entity<Listing>().ToTable("listing");
            modelBuilder.Entity<Transaction>().ToTable("transaction");
            modelBuilder.Entity<Manufacturer>().ToTable("manufacturer");
            modelBuilder.Entity<History>().ToTable("history");
        }
	}

    public class History
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(36)")]
        public Guid EntityId { get; set; }

        [MaxLength(56)]
        public string Entity { get; set; }

        public string Data { get; set; }

        public OperationType Operation { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class Marketplace : Trackable
    {
        [Key]
        [Column(TypeName = "varchar(36)")]
        public Guid Id { get; set; }
    }

    public abstract class Trackable
    {
        
        static Trackable()
        {
            Triggers<Trackable, AppDbContext>.Inserted += entry => 
            {
                History history = ToHistory(entry, OperationType.Insert);

                entry.Context.History.Add(history);
                entry.Context.SaveChanges();
            };
            Triggers<Trackable, AppDbContext>.Updated += entry =>
            {
                History history = ToHistory(entry, OperationType.Update);

                entry.Context.History.Add(history);
                entry.Context.SaveChanges();
            };
            Triggers<Trackable, AppDbContext>.Deleted += entry =>
            {
                History history = ToHistory(entry, OperationType.Delete);

                entry.Context.History.Add(history);
                entry.Context.SaveChanges();
            };

            History ToHistory(IAfterEntry entry, OperationType type)
            {
                var entity = entry.Entity as Marketplace;

                return new History()
                { 
                    Operation   = type, 
                    Entity      = entry.Entity.ToString(),
                    EntityId    = entity.Id,
                    Data        = JsonConvert.SerializeObject(entry.Entity),
                    CreatedOn   = DateTime.Now
                };
            }
        }
    }

    public enum OperationType
    {
        Insert = 1,
        Update = 5,
        Delete = 9
    }
}
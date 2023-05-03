using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TastyPoint.API.Publishing.Domain.Models;
using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Ordering.Domain.Models;

using TastyPoint.API.Security.Domain.Models;
using TastyPoint.API.Profiles.Domain.Models;
using TastyPoint.API.Profiles.Resources;
using TastyPoint.API.Shared.Extensions;
using TastyPoint.API.Subscription.Domain.Models;
using TastyPoint.API.Social.Domain.Models;

namespace TastyPoint.API.Shared.Persistence.Contexts;

public class AppDbContext: DbContext
{
    public DbSet<Pack> Packs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<BusinessPlan> BusinessPlans { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<FoodStore> FoodStores { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //Product Entity Mapping Configuration
        builder.Entity<Product>().ToTable("Products");
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Product>().Property(p => p.Type).IsRequired().HasMaxLength(50);

        //Pack Entity Mapping Configuration
        builder.Entity<Pack>().ToTable("Packs");
        builder.Entity<Pack>().HasKey(p => p.Id);
        builder.Entity<Pack>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Pack>().Property(p => p.Name).IsRequired().HasMaxLength(100);

        //BusinessPlans Mapping Configuration
        builder.Entity<BusinessPlan>().ToTable("BusinessPlans");
        builder.Entity<BusinessPlan>().HasKey(p => p.Id);
        builder.Entity<BusinessPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BusinessPlan>().Property(p => p.CurrentPlan).IsRequired().HasMaxLength(100);
        builder.Entity<BusinessPlan>().Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Entity<BusinessPlan>().Property(p => p.PlanPrice).IsRequired();

        //Comment Entity Mapping Configuration
        builder.Entity<Comment>().ToTable("Comments");
        builder.Entity<Comment>().HasKey(p => p.Id);
        builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Comment>().Property(p => p.Rate).IsRequired();
        builder.Entity<Comment>().Property(p => p.Text).IsRequired().HasMaxLength(500);

        //FoodStore Entity Mapping Configuration
        builder.Entity<FoodStore>().ToTable("FoodStores");
        builder.Entity<FoodStore>().HasKey(p => p.Id);
        builder.Entity<FoodStore>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<FoodStore>().Property(p => p.Address).HasMaxLength(250);
        builder.Entity<FoodStore>().Property(p => p.Description).HasMaxLength(1280);
        builder.Entity<FoodStore>().Property(p => p.Favorite).IsRequired().HasDefaultValue(false);
        builder.Entity<FoodStore>().Property(p => p.Image).HasMaxLength(250);
        builder.Entity<FoodStore>().Property(p => p.Name).HasMaxLength(50);
        builder.Entity<FoodStore>().Property(p => p.Rate);

        //Promotion Entity Mapping Configuration
        builder.Entity<Promotion>().ToTable("Promotions");
        builder.Entity<Promotion>().HasKey(p => p.Id);
        builder.Entity<Promotion>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Promotion>().Property(p => p.Title).IsRequired().HasMaxLength(300);
        builder.Entity<Promotion>().Property(p => p.SubTitle).HasMaxLength(300);
        builder.Entity<Promotion>().Property(p => p.Description).HasMaxLength(500);
        builder.Entity<Promotion>().Property(p => p.Image).HasMaxLength(500);

        //Order Entity Mapping Configuration
        builder.Entity<Order>().ToTable("Orders");
        builder.Entity<Order>().HasKey(p => p.Id);
        builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Order>().Property(p => p.Status).IsRequired().HasMaxLength(100);
        builder.Entity<Order>().Property(p => p.DeliveryMethod).IsRequired().HasMaxLength(100);
        builder.Entity<Order>().Property(p => p.PaymentMethod).IsRequired().HasMaxLength(100);

        // User Entity Mapping Configuration
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(30);

        //User Profile Mapping Configuration
        builder.Entity<UserProfile>().ToTable("UserProfiles");
        builder.Entity<UserProfile>().HasKey(p => p.Id);
        builder.Entity<UserProfile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<UserProfile>().Property(p => p.Name).HasMaxLength(100);
        builder.Entity<UserProfile>().Property(p => p.Type).IsRequired().HasMaxLength(30);
        builder.Entity<UserProfile>().Property(p => p.PhoneNumber).HasMaxLength(30);

        //Relationships
        builder.Entity<Pack>()
            .HasMany(p => p.Products)
            .WithOne(p => p.Pack)
            .HasForeignKey(p => p.PackId);

        builder.Entity<FoodStore>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.FoodStore)
            .HasForeignKey(p => p.FoodStoreId);

        builder.Entity<UserProfile>()
            .HasOne(p => p.User)
            .WithOne(p => p.UserProfile)
            .HasForeignKey<UserProfile>(p => p.UserId);

        builder.Entity<UserProfile>()
            .HasMany(p => p.Orders)
            .WithOne(p => p.UserProfile)
            .HasForeignKey(p => p.UserProfileId);

        builder.Entity<FoodStore>()
            .HasOne(p => p.UserProfile)
            .WithOne(p => p.FoodStore)
            .HasForeignKey<FoodStore>(p => p.UserProfileId);

        builder.Entity<UserProfile>()
            .HasMany(p => p.Promotions)
            .WithOne(p => p.UserProfile)
            .HasForeignKey(p => p.UserProfileId);

        builder.Entity<UserProfile>()
            .HasMany(p => p.Packs)
            .WithOne(p => p.UserProfile)
            .HasForeignKey(p => p.UserProfileId);
        
    base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();
    }
}
using Domain.Entities;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Views;
using Domain.Common;
using System;
using Application.Common.CurrentUser;
using System.Reflection.Emit;
using Domain.Entities.Orders;
using Domain.Entities.Carts;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<AuthUser, Role, long,
        IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions options
            , ICurrentUserService currentUserService) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _currentUserService = currentUserService;
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<PricingItem> PricingItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DiscountItem> DiscountItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<VwSearch> VwSearches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuthUser>()
              .HasMany(ur => ur.UserRoles)
              .WithOne(u => u.User)
              .HasForeignKey(ur => ur.UserId)
              .IsRequired();

            builder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
                    
            builder.Entity<VwSearch>()
                .HasNoKey()
                .ToView("VwSearch");


            #region Default query filter
            builder.Entity<Category>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
            builder.Entity<SubCategory>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
            builder.Entity<Product>().HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
            
            #endregion

            builder.ApplyUtcDateTimeConverter();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _currentUserService.UserId;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedById = _currentUserService.UserId;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        entry.Entity.DeletedOn = DateTime.UtcNow;
                        entry.Entity.DeletedById = _currentUserService.UserId;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }


            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _currentUserService.UserId;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedById = _currentUserService.UserId;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        entry.Entity.DeletedOn = DateTime.UtcNow;
                        entry.Entity.DeletedById = _currentUserService.UserId;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }


            return base.SaveChanges();
        }


    }
    public static class UtcDateAnnotation
    {
        private const String IsUtcAnnotation = "IsUtc";
        private static readonly ValueConverter<DateTime, DateTime> UtcConverter =
          new ValueConverter<DateTime, DateTime>(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableConverter =
          new ValueConverter<DateTime?, DateTime?>(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

        public static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder, Boolean isUtc = true) =>
          builder.HasAnnotation(IsUtcAnnotation, isUtc);

        public static Boolean IsUtc(this IMutableProperty property) =>
          ((Boolean?)property.FindAnnotation(IsUtcAnnotation)?.Value) ?? true;

        /// <summary>
        /// Make sure this is called after configuring all your entities.
        /// </summary>
        public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (!property.IsUtc())
                    {
                        continue;
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(UtcConverter);
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(UtcNullableConverter);
                    }
                }
            }
        }
    }
}

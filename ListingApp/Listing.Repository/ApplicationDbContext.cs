using Listing.Domain.DomainModels;
using Listing.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Listing.Repository
{
    public class ApplicationDbContext : IdentityDbContext<UserDetails>
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ListingPost> Listings { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        public virtual DbSet<ListingsInWishlist> ListingsInWishlist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            /*  builder.Entity<Category>()
                .Property(z => z.Name)
                .IsRequired();

            builder.Entity<Image>()
                .Property(z => z.ImageData)
                .IsRequired();

            builder.Entity<Image>()
                .Property(z => z.MimeType)
                .IsRequired();

            builder.Entity<ListingPost>()
                .Property(z => z.Title)
                .IsRequired();

            builder.Entity<ListingPost>()
                .Property(z => z.Price)
                .IsRequired();  */

            /*     builder.Entity<UserDetails>()
                     .Property(z => z.Contact)
                     .IsRequired();

                 builder.Entity<UserDetails>()
                     .Property(z => z.FirstName)
                     .IsRequired();

                 builder.Entity<UserDetails>()
                     .Property(z => z.LastName)
                     .IsRequired();*/

            // One-to-One Relationships
         /*   builder.Entity<UserDetails>()
                .HasOne(z => z.Image)
                .WithOne(z => z.UserImage)
                .HasForeignKey<UserDetails>(z => z.ImageId);*/

            builder.Entity<Wishlist>()
               .HasOne(z => z.Owner)
               .WithOne(z => z.UserWishlist)
               .HasForeignKey<Wishlist>(z => z.OwnerId);

            // One-to-Many Relationships
            builder.Entity<Image>()
                .HasOne<ListingPost>(z => z.Listing)
                .WithMany(z => z.ListingImages)
                .HasForeignKey(z => z.ListingId);

            builder.Entity<ListingPost>()
               .HasOne<Category>(z => z.Category)
               .WithMany(z => z.ListingPosts)
               .HasForeignKey(z => z.CategoryId);

            builder.Entity<ListingPost>()
                .HasOne<Location>(z => z.Location)
                .WithMany(z => z.ListingPosts)
                .HasForeignKey(z => z.LocationId);

            // Many-to-Many Relationship
            builder.Entity<ListingsInWishlist>()
                .HasOne<Wishlist>(z => z.Wishlist)
                .WithMany(z => z.ListingsInWishlists)
                .HasForeignKey(z => z.WishlistId);

            builder.Entity<ListingsInWishlist>()
                .HasOne<ListingPost>(z => z.Listing)
                .WithMany(z => z.ListingsInWishlists)
                .HasForeignKey(z => z.ListingId);

           
        }
    }
}

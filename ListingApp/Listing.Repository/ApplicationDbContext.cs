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
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }


        public virtual DbSet<ListingsInWishlist> ListingsInWishlist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One-to-One Relationships       
            builder.Entity<UserImage>()
                 .HasOne(z => z.Image)
                 .WithOne(z => z.Image)
                 .HasForeignKey<UserImage>(z => z.UserId);

            builder.Entity<ListingImage>()
              .HasOne<ListingPost>(z => z.Listing)
              .WithMany(z => z.ListingImages)
              .HasForeignKey(z => z.ListingId);

            builder.Entity<Wishlist>()
               .HasOne(z => z.Owner)
               .WithOne(z => z.UserWishlist)
               .HasForeignKey<Wishlist>(z => z.OwnerId);

 
            // One-to-Many Relationships
            builder.Entity<ListingImage>()
                .HasOne<ListingPost>(z => z.Listing)
                .WithMany(z => z.ListingImages)
                .HasForeignKey(z => z.ListingId);

            builder.Entity<Comment>()
                .HasOne<ListingPost>(z => z.Listing)
                .WithMany(z => z.Comments)
                .HasForeignKey(z => z.ListingId);

            builder.Entity<ListingPost>()
               .HasOne<Category>(z => z.Category)
               .WithMany(z => z.ListingPosts)
               .HasForeignKey(z => z.CategoryId);

            builder.Entity<ListingPost>()
                .HasOne<Location>(z => z.Location)
                .WithMany(z => z.ListingPosts)
                .HasForeignKey(z => z.LocationId);

            builder.Entity<Message>()
                .HasOne<UserDetails>(z => z.User)
                .WithMany(z => z.UserMessages)
                .HasForeignKey(z => z.UserId);

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

namespace DataLayer.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Authors)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Books>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Books)
                .HasForeignKey(e => e.BookId);

            modelBuilder.Entity<Books>()
                .HasMany(e => e.Rating)
                .WithOptional(e => e.Books)
                .HasForeignKey(e => e.BookId);

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Genres)
                .HasForeignKey(e => e.GenreId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.UserId);
        }
    }
}

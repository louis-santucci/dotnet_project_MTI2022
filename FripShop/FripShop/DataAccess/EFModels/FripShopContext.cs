using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FripShop.DataAccess.EFModels
{
    public partial class FripShopContext : DbContext
    {
        public FripShopContext()
        {
        }

        public FripShopContext(DbContextOptions<FripShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4MS830T\\MTI;Initial Catalog=FripShop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(20)
                    .HasColumnName("brand");

                entity.Property(e => e.Category)
                    .HasMaxLength(30)
                    .HasColumnName("category");

                entity.Property(e => e.Condition).HasColumnName("condition");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.ImageSource)
                    .HasMaxLength(100)
                    .HasColumnName("imageSource");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SellerId).HasColumnName("sellerId");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .HasColumnName("sex");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("state");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Article_User");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.BuyerId).HasColumnName("buyerId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Cart_Article");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.BuyerId).HasColumnName("buyerId");

                entity.Property(e => e.LastUpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdateAt");

                entity.Property(e => e.TransactionState)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("transactionState");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Article");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Id, "IX_User")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "IX_User_1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.NbNotes)
                    .HasDefaultValue(0)
                    .HasColumnName("nbNote");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LNCWebApp.Data;
using LNCLibrary.Models;
using LNCWebApp.Models;

namespace LNCLibrary.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LNCLibrary.Models.CartItems", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cartID");

                    b.Property<string>("itempicture");

                    b.Property<string>("name");

                    b.Property<int>("price");

                    b.Property<int>("productID");

                    b.HasKey("ID");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("LNCLibrary.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CartID");

                    b.Property<string>("CheckoutComments");

                    b.Property<string>("City");

                    b.Property<string>("ConfirmationNumber");

                    b.Property<DateTime>("DateOfPurchase");

                    b.Property<string>("Email");

                    b.Property<float>("FinalPrice");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("PaymentMethod");

                    b.Property<string>("RegularCustomerId");

                    b.Property<int>("State");

                    b.Property<int>("Status");

                    b.Property<int>("ZipCode");

                    b.Property<bool>("isGuest");

                    b.HasKey("ID");

                    b.HasIndex("RegularCustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LNCLibrary.Models.OrderDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CartID");

                    b.Property<string>("Name");

                    b.Property<int>("OrderID");

                    b.Property<int>("price");

                    b.Property<string>("productpicture");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("LNCLibrary.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("GenderOption");

                    b.Property<int>("Price");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductName");

                    b.Property<string>("ProfilePicture");

                    b.Property<int>("Quantity");

                    b.HasKey("ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LNCLibrary.Models.Size", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ProductID");

                    b.Property<int>("ThisSize");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("LNCWebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("CheckoutComments");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("State");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LNCLibrary.Models.Order", b =>
                {
                    b.HasOne("LNCWebApp.Models.ApplicationUser", "RegularCustomer")
                        .WithMany("OrderHistory")
                        .HasForeignKey("RegularCustomerId");
                });

            modelBuilder.Entity("LNCLibrary.Models.OrderDetails", b =>
                {
                    b.HasOne("LNCLibrary.Models.Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LNCLibrary.Models.Size", b =>
                {
                    b.HasOne("LNCLibrary.Models.Product")
                        .WithMany("AvailableSizes")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LNCWebApp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LNCWebApp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LNCWebApp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

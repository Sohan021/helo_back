﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ofarz_rest_api.Persistence.Context;

namespace ofarz_rest_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201102091446_sdad")]
    partial class sdad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Account.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Account.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AgentCode");

                    b.Property<string>("AgentShopName");

                    b.Property<string>("ApplicationRoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("CountryId");

                    b.Property<string>("CountryName");

                    b.Property<int?>("DistrictId");

                    b.Property<string>("DistrictName");

                    b.Property<int?>("DivisionId");

                    b.Property<string>("DivisionName");

                    b.Property<int>("DownlineCount");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("FifthLevelDownlineCount");

                    b.Property<int>("FirstLevelDownlineCount");

                    b.Property<string>("FirstName");

                    b.Property<int>("FourthLevelDownlineCount");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MarketCode");

                    b.Property<int?>("MarketId");

                    b.Property<string>("MarketName");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("NID_Number");

                    b.Property<string>("Nominee_Name");

                    b.Property<string>("Nominee_PhonNumber");

                    b.Property<string>("Nominee_Relation");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PostalCode");

                    b.Property<string>("ProfilePhoto");

                    b.Property<string>("RefferName");

                    b.Property<string>("ReffrerId");

                    b.Property<string>("ReffrerName");

                    b.Property<int>("SecondLevelDownlineCount");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("ThirdLevelDownlineCount");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int?>("UnionOrWardId");

                    b.Property<string>("UnionOrWardName");

                    b.Property<int?>("UpozilaId");

                    b.Property<string>("UpozilaName");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationRoleId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("MarketId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("ReffrerId");

                    b.HasIndex("UnionOrWardId");

                    b.HasIndex("UpozilaId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.AgentFund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgentId");

                    b.Property<string>("AgentName");

                    b.Property<string>("AgentPhoneNumber");

                    b.Property<double>("MainAccount");

                    b.Property<double>("SellViaDirectCash");

                    b.Property<double>("ShoperTransection");

                    b.Property<double>("TotalTransection");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.ToTable("AgentFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.CeoFund", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CeoCode");

                    b.Property<string>("CeoId");

                    b.Property<double>("MainAccount");

                    b.Property<double>("TotalIncome");

                    b.HasKey("Id");

                    b.HasIndex("CeoId");

                    b.ToTable("CeoFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.KarrotFund", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KarrotCode");

                    b.Property<string>("KarrotId");

                    b.Property<double>("MainAccount");

                    b.Property<double>("TotalIncome");

                    b.HasKey("Id");

                    b.HasIndex("KarrotId");

                    b.ToTable("KarrotFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.OfarzFund", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("GetMoneyByAgent");

                    b.Property<double>("GetMoneyByAgentShopping");

                    b.Property<double>("GetMoneyByAppSharer");

                    b.Property<double>("GetMoneyByCeo");

                    b.Property<double>("GetMoneyByKarrot");

                    b.Property<double>("MainAccount");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("OfarzId");

                    b.HasKey("Id");

                    b.HasIndex("OfarzId");

                    b.ToTable("OfarzFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgentPhnNumber");

                    b.Property<double>("Amount");

                    b.Property<string>("PayerId");

                    b.Property<string>("PayerName");

                    b.Property<string>("PayerPhoneNumber");

                    b.Property<DateTime>("PaymentTime");

                    b.Property<int>("PaymentTypeId");

                    b.Property<int>("ProductTypeId");

                    b.HasKey("Id");

                    b.HasIndex("PayerId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PaymentTypeDescription");

                    b.Property<string>("PaymentTypeName");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.SharerFund", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BackShoppingAccount");

                    b.Property<double>("MainAccount");

                    b.Property<string>("SharerId");

                    b.Property<string>("SharerName");

                    b.Property<string>("SharerPhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("SharerId");

                    b.ToTable("SharerFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.ShoperFund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BackShoppingAccount");

                    b.Property<string>("ShoperId");

                    b.Property<string>("ShoperName");

                    b.Property<string>("ShoperPhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("ShoperId");

                    b.ToTable("ShoperFunds");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.WithdrawMoney", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgentPhnNumber");

                    b.Property<double>("Amount");

                    b.Property<string>("CurrentUserId");

                    b.Property<string>("OfarzPhoneNumber");

                    b.Property<DateTime>("PaymentTime");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WithdrawMoney");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.AgentOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderNo");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("RoleId");

                    b.Property<double>("TotalAmount");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AgentOrders");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryCode");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DistrictCode");

                    b.Property<int?>("DivisionId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<string>("DivisionCode");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MarketCode");

                    b.Property<string>("Name");

                    b.Property<int?>("UnionOrWardId");

                    b.HasKey("Id");

                    b.HasIndex("UnionOrWardId");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderNo");

                    b.Property<string>("PhoneNo");

                    b.Property<string>("RoleId");

                    b.Property<double>("TotalAmount");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AgentOrderId");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("AgentOrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<int>("CountInStock");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsAvailabe");

                    b.Property<string>("Name");

                    b.Property<float>("Price");

                    b.Property<string>("ProductCode");

                    b.Property<int?>("ProductTypeId");

                    b.Property<int?>("SubCategoryId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.UnionOrWard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("UnionOrWardCode");

                    b.Property<int?>("UpozilaId");

                    b.HasKey("Id");

                    b.HasIndex("UpozilaId");

                    b.ToTable("UnionOrWards");
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Upozila", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DistrictId");

                    b.Property<string>("Name");

                    b.Property<string>("UpozilaCode");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Upozillas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Account.ApplicationUser", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationRole", "ApplicationRole")
                        .WithMany()
                        .HasForeignKey("ApplicationRoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Market", "Market")
                        .WithMany()
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Reffrer")
                        .WithMany()
                        .HasForeignKey("ReffrerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.UnionOrWard", "UnionOrWard")
                        .WithMany()
                        .HasForeignKey("UnionOrWardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Upozila", "Upozilla")
                        .WithMany()
                        .HasForeignKey("UpozilaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.AgentFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Agent")
                        .WithMany()
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.CeoFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Ceo")
                        .WithMany()
                        .HasForeignKey("CeoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.KarrotFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Karrot")
                        .WithMany()
                        .HasForeignKey("KarrotId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.OfarzFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Ofarz")
                        .WithMany()
                        .HasForeignKey("OfarzId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.Payment", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Payer")
                        .WithMany()
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Fund.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.SharerFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Sharer")
                        .WithMany()
                        .HasForeignKey("SharerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.ShoperFund", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "Shoper")
                        .WithMany()
                        .HasForeignKey("ShoperId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.Fund.WithdrawMoney", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.AgentOrder", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Market", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.District", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Division", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Market", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.UnionOrWard", "UnionOrWard")
                        .WithMany()
                        .HasForeignKey("UnionOrWardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Order", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Market", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.Account.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.OrderDetail", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.AgentOrder", "AgentOrder")
                        .WithMany("OrderDetails")
                        .HasForeignKey("AgentOrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Product", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ofarz_rest_api.Domain.Models.User.SubCategory", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.UnionOrWard", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.Upozila", "Upozila")
                        .WithMany()
                        .HasForeignKey("UpozilaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ofarz_rest_api.Domain.Models.User.Upozila", b =>
                {
                    b.HasOne("ofarz_rest_api.Domain.Models.User.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

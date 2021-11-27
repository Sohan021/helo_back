using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.Fund;
//using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.User;
using System.Linq;

namespace ofarz_rest_api.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {

        }


        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }


        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<SharerFund> SharerFunds { get; set; }
        public virtual DbSet<AgentFund> AgentFunds { get; set; }
        public virtual DbSet<ShoperFund> ShoperFunds { get; set; }


        public virtual DbSet<OfarzFund> OfarzFunds { get; set; }
        public virtual DbSet<KarrotFund> KarrotFunds { get; set; }
        public virtual DbSet<CeoFund> CeoFunds { get; set; }


        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<WithdrawMoney> WithdrawMoney { get; set; }



        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<AgentOrder> AgentOrders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<AgentOrderDetails> AgentOrderDetails { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Upozila> Upozillas { get; set; }
        public virtual DbSet<UnionOrWard> UnionOrWards { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Market> Markets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }



        }
    }
}

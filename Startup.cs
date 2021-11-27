using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IAccountRepositories;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
//using ofarz_rest_api.Domain.IRepositories.IAccountRepositories;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.IService.IAccountServices;
using ofarz_rest_api.Domain.IService.IFundServices;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.Account;
//using ofarz_rest_api.Options;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Persistence.Repositories;
using ofarz_rest_api.Persistence.Repositories.AccountRepositories;
using ofarz_rest_api.Persistence.Repositories.FundRepositories;
//using ofarz_rest_api.Persistence.Repositories.AccountRepositories;
using ofarz_rest_api.Persistence.Repositories.UserRepositories;
using ofarz_rest_api.Persistence.UserProvider;
using ofarz_rest_api.Services.AccountServices;
using ofarz_rest_api.Services.FundServices;
//using ofarz_rest_api.Services.AccountServices;
using ofarz_rest_api.Services.UserServices;
using System;
//using Swashbuckle.AspNetCore.Swagger;
using System.IdentityModel.Tokens.Jwt;
//using Toycloud.AspNetCore.Mvc.ModelBinding;

namespace ofarz_rest_api
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                // options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;


            });


            //var connectionString = Configuration["ConnectionString:sw"];
            //services.AddDbContext<AppDbContext>(c => c.UseSqlServer(connectionString));

            var sqlConnectionString = Configuration.GetConnectionString("ofarz");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(sqlConnectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                    option =>
                    {
                        option.Password.RequireDigit = false;
                        option.Password.RequiredLength = 6;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                    }
                ).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes
                                                       (Configuration["Token:Key"]))
                };
            });

            services.AddMvc();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(c =>
                    c.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDataProtection();

            //services.AddMvc(options =>
            //{
            //    options.ModelBinderProviders.InsertBodyOrDefaultBinding();
            //});

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000", "http://localhost:3001")
                    .AllowAnyHeader().AllowCredentials()
                    .AllowAnyMethod();
            }));


            services.AddHttpContextAccessor();



            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductTypeService, ProductTypeService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IDivisionService, DivisionService>();

            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictService, DistrictService>();

            services.AddScoped<IUpozillaRepository, UpozillaRepository>();
            services.AddScoped<IUpozillaService, UpozilaService>();

            services.AddScoped<IUnionOrWardRepository, UnionOrWardRepository>();
            services.AddScoped<IUnionService, UnionService>();

            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IMarketService, MarketService>();







            //services.AddScoped<IAdminRepository, AdminRepository>();
            //services.AddScoped<IAdminService, AdminService>();

            //services.AddScoped<IModeratorRepository, ModeratorRepository>();
            //services.AddScoped<IModeratorService, ModeratorService>();

            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IRoleService, RoleService>();

            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IRoleService, RoleService>();



            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();



            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();

            services.AddScoped<IAgentFundRepository, AgentFundRepository>();
            services.AddScoped<IAgentFundService, AgentFundService>();

            services.AddScoped<ICustomerFundRepository, CustomerFundRepository>();
            services.AddScoped<ICustomerFundService, CustomerFundService>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<ISharerFundRepository, SharerFundRepository>();
            services.AddScoped<ISharerFundService, SharerFundService>();

            services.AddScoped<IWithdrawMoneyRepository, WithdrawMoneyRepository>();
            services.AddScoped<IWithdrawMoneyService, WithdrawMoneyService>();




            //services.AddScoped<ValidateMimeMultipartContentFilter>();

            services.AddScoped<IUserProvider, UserProvider>();

            //services.AddSwaggerGen(x =>
            //{
            //    x.SwaggerDoc("v1", new Info { Title = "swapi", Version = "v1" });
            //});

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            //var swaggerOptions = new SwaggerOption();
            //Configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOptions);

            //app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            //app.UseSwaggerUI(option =>
            //{
            //    option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);

            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}

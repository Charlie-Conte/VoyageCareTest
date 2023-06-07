using DapperExtensions.Mapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MudBlazor.Services;
using StaffPortal.DataAccess.Auth;
using Microsoft.EntityFrameworkCore;
using StaffPortal.Logic;


namespace StaffPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            GlobalHelper.CoreConnectionString = connectionString;
            //DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(setupAction: options =>  {
            //        options.SignIn.RequireConfirmedAccount = false;
            //        options.User.RequireUniqueEmail = true;
            //    })              
            //    .AddRoleStore<RoleStore<IdentityRole>>()                    
            //    .AddUserStore<UserStore<IdentityUser>>();

            //builder.Services.AddScoped<IUserStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserPasswordStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserEmailStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserLoginStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserRoleStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserClaimStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserPhoneNumberStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserLockoutStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IUserTwoFactorStore<IdentityUser>, UserStore<IdentityUser>>();
            //builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(setupAction: options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.User.RequireUniqueEmail = true;
            //})
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<VoyageCareSignInManager<IdentityUser>>();

            builder.Services.AddScoped<ILogInAccountManager<IdentityUser>, AspAccountManager>();
                ;
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddMudServices();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
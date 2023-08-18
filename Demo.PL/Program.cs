using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.Mapping_Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServicesThatAllowDependancyInjection
            builder.Services.AddControllersWithViews();

            // Not Best way to write ConnectionString || Best way to wirte it in AppSettings File
            builder.Services.AddDbContext<Session02DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #region Dependancy Injection
            /*
             * services.AddTransient<>();
             *      - Awl lma n5ls el Operation el object da byt4al mn heap 
             *      
             * services.AddScoped<>();
             *      - Staying at Heap[Memory] While Request is Running
             *      - Per Request
             * services.AddSingleton<>();
             *      - tool ma el user fat7 el app
             *          - Caching Services
             *          - Log     Services 
             */
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Dependncy Injection in Department
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();     // Dependncy Injection in Employee

            // Allowed Dependncy Injection for UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // lma 7d ytlb object mn class IUnitOfWork e3ml create hna

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));

            // Security Module
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true; // Allow @#$
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<Session02DbContext>()
                .AddDefaultTokenProviders(); // to add Token

            // Allow Dependancy Injection for UserManager | SignInManager | Manager
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";        // if token Correct go to Login
                    options.AccessDeniedPath = "Home/Error";    // if token Invalid go to Error
                });


            #endregion
            #endregion

            var app = builder.Build();

            #region Config HTTP Request Pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection(); // Redirect HTTP Request to HTTPS
            app.UseStaticFiles();      // Using Bootstrap and other files in Project
                                       // Using Resources Files in Project

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });



            #endregion

            app.Run();

        }



        /* Partial View :
         *      - lw 3ndy part fe el code by7slo tkrar f ha5od el part da w a7oto fe Partial View
         *      - Create PartialView on Folder "Shared"
         */

        /*
         * Security Module :
         * ===================
         * Step 0 : Identification [Registeration]
         *      - Have Account at Web App
         * -----------------------------------------------------------------------------------
         * Step 1 : Authentication
         *      - Insert Email,Password [Who Are U ? ]
         *      - Where are You Come?
         *          # Local [Create Account in Web App]
         *          # Active Directory [Must be User Connected to Another User Have Access]
         *          # External Server [Facebook , Google ,....]
         *          # Fedrated Server [Souq -> Amazon]
         * -----------------------------------------------------------------------------------
         * Step 2 : Authorization
         *      - What Can i Do ?
         *          # Roles [Each User has its own Roles | Uber => User Role , Captain Role]
         *          # Relation Between User and Role [Many to Many]
         * -----------------------------------------------------------------------------------
         * Security at ASP.NET Core Using ==> Microsoft Identity Package :
         * ================================================================
         *      # Identification [ Registeration ] : UserManager (IdentityUser)
         * 
         *          1. Create User [SignUp]
         *          2. Update User
         *          3. Delete User
         *          4. Read User Data
         *          5. Confirm Account
         * -----------------------------------------------------------------------------------
         *      # Authentication : SignInManager (IdentityUser)
         * 
         *          1. Sign In
         *          2. Sign Out
         *          3. IsSigned
         *          4. Reset Password
         *          5. Two Factor Authentication
         *          6. OTP Authentication
         *          7. External Login [Face , Google,..]
         * -----------------------------------------------------------------------------------
         *      # Authorization : RoleManager (IdentityRole)
         * 
         *          1. Create Role
         *          2. Update Role
         *          3. Delete Role
         * -----------------------------------------------------------------------------------
         * #1 Install the Package (Microsoft Identity Package) in DataAccessLayer DAL
         * 
         * #2 Make DbSet<> For Class Users | Roles in DbContext Class // Not Important
         * 
         * #3 Make Class Session02DbContext Inherit from Class "IdentityDbContext"
         *      
         * 
         * 
         * 
         * 
         * 
         * 
         */





    }
}

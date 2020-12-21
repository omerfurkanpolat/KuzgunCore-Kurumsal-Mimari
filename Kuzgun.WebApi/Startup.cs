using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Concrete.Managers;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Core.Utilities.EmailService.Smtp.Google;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.DataAccess.Concrete;
using Kuzgun.DataAccess.Concrete.EntityFramework;
using Kuzgun.Entities.Concrete;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KuzgunContext>(x =>
                x.UseSqlServer(@"server=(localdb)\mssqllocaldb;Database=KuzgunContext; Trusted_Connection=true"));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<KuzgunContext>().AddDefaultTokenProviders();
            ;
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;


            });
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IMessageDal, EfMessageDal>();

            services.AddScoped<IPostCommentService, PostCommentManager>();
            services.AddScoped<IPostCommentDal, EfPostCommentDal>();

            services.AddScoped<IPostService, PostManager>();
            services.AddScoped<IPostDal, EfPostDal>();

            services.AddScoped<IPostStatService, PostStatManager>();
            services.AddScoped<IPostStatDal, EfPostStatDal>();

            services.AddScoped<ISubCategoryService, SubCategoryManager>();
            services.AddScoped<ISubCategoryDal, EfSubCategoryDal>();

            services.AddScoped<IEmailService, GoogleEmailService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

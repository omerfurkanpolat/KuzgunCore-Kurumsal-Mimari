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
using Kuzgun.Core.DependencyResolver;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Extensions;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Core.Utilities.EmailService.Smtp.Google;
using Kuzgun.Core.Utilities.IoC;
using Kuzgun.Core.Utilities.Security.Encryption;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.DataAccess.Concrete;
using Kuzgun.DataAccess.Concrete.EntityFramework;
using Kuzgun.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TokenOptions = Kuzgun.Core.Utilities.Security.Jwt.TokenOptions;

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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                    builder => builder.WithOrigins("http://localhost:4200"));
            });
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependecyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });
            

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
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

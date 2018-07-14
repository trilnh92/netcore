using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services;
using WebBlog.Services.IServices;
using WebBlog.Services.Services;

namespace WebBlog.Api
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
            //This is not good solution. Should find another solution
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("WebBlogIdentityDBConnection")));

            services.AddDbContext<WebBlogDbContext>(options => options.UseMySql(Configuration.GetConnectionString("WebBlogDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMediaService, MediaService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IFileProvider>(
              new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WebBlog API",
                    Description = "ASP.NET Core 2.0 WebBlog API",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Tai Hoang",
                        Email = "tai.hoangvan@nashtechglobal.com",
                        Url = "https://sd2169.azurewebsites.net"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebBlog API");
            });

            app.UseCors("AllowAllHeaders");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SpaServices.Webpack;
using WebBlog.Database.Data;
using WebBlog.Database.Models;
using WebBlog.Services.Services;
using WebBlog.Services.IServices;
using WebBlog.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace WebBlog
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("WebBlogIdentityDBConnection")));

            services.AddDbContext<WebBlogDbContext>(options => options.UseMySql(Configuration.GetConnectionString("WebBlogDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            //ISSUE: System.Security.Cryptography.CryptographicException: The key  was not found in the key ring. 
            //at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector.UnprotectCore at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector.DangerousUnprotect 
            //at Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector.Unprotect 
            //at Microsoft.AspNetCore.Antiforgery.Internal.DefaultAntiforgeryTokenSerializer.Deserialize
            //This issue occurred because when deploy on google cloud platform, there is 2 instances to load balancing for app engine. It use 1 keys and kets not crypted and share together :( :(            

            //Redis cache: Cannot connect to redis cache server :( :(
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "10.0.0.3";
            //    option.InstanceName = "rediswebblog";
            //});
            //var redis = ConnectionMultiplexer.Connect("10.0.0.3:6379");
            //services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Keys");

            //var redis = ConnectionMultiplexer.Connect("10.0.0.3:6379");
            //services.AddDataProtection();
            //services.Configure<KeyManagementOptions>(o =>
            //{
            //    o.XmlRepository = new RedisXmlRepository(() => redis.GetDatabase(), "DataProtection-Keys");
            //});

            //Disable automatickey: ----> Not work :(
            //services.AddDataProtection()
            //        .DisableAutomaticKeyGeneration();

            //Custom crypt: ----> Not work :(
            //services.AddDataProtection()
            //    .UseCryptographicAlgorithms(
            //    new AuthenticatedEncryptorConfiguration()
            //    {
            //        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            //        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //    });

            //Save persistkeys to file: -----> Not work :(
            //services.AddDataProtection()
            //    .SetApplicationName("admin-webblog")
            //    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"./"));

            //My temp solution: I setup 1 instance in app.yaml for the deployment ----> WORKED :)

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

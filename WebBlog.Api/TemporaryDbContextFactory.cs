using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Database.Data;

namespace WebBlog.Api
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<WebBlogDbContext>
    {
        //////// 
        public WebBlogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WebBlogDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();

            builder.UseSqlServer(configuration.GetConnectionString("WebBlogDBConnection"));
            return new WebBlogDbContext(builder.Options);
        }
    }
}

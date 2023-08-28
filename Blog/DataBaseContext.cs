using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog
{
    public class DataBaseContext : DbContext
    {
            
            private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseLoggerFactory(_loggerFactory);
                optionsBuilder.UseSqlServer("Server=localhost\\SASHASQL;Database=Blog;Trusted_Connection=true;TrustServerCertificate=True;");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<TextBlog>().Property(x => x.Date).HasColumnType("smalldatetime");

            modelBuilder.Entity<Users>().HasKey(x => x.Login);
            modelBuilder.Entity<TextBlog>().HasKey(x => x.Id);
            modelBuilder.Entity<Coments>().HasKey(x => x.IdComment);
            modelBuilder.Entity<Like>().HasKey(x => x.IdLike);
            modelBuilder.Entity<Subs>().HasKey(x => x.Id);
            
            modelBuilder.Entity<TextBlog>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Subs>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Coments>().Property(x => x.IdComment).UseIdentityColumn();
            modelBuilder.Entity<Like>().Property(x => x.IdLike).UseIdentityColumn();
           
            }

            public DbSet<Users> Users { get; set; }
            public DbSet<Subs> Subs { get; set; }
            public DbSet<TextBlog> Text { get; set; }
            public DbSet<Like> Like { get; set; }
            public DbSet<Coments> Coments { get; set; }


        
    }
}

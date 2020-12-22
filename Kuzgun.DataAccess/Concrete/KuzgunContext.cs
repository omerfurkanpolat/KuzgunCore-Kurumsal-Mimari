using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.DataAccess.Concrete.Mapping;
using Kuzgun.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.DataAccess.Concrete
{
    public class KuzgunContext:IdentityDbContext<User,Role, int>
    {
        public KuzgunContext()
        {
            
        }
        public KuzgunContext(DbContextOptions<KuzgunContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=KuzgunContext;integrated security=true;");
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostStat> PostStats { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new MessageMap());
            builder.ApplyConfiguration(new PostCommentMap());
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new PostStatMap());
            builder.ApplyConfiguration(new SubCategoryMap());
            builder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(builder);


        }
    }
}

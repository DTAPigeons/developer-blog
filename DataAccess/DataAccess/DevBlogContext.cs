using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess {
    public class DevBlogContext: DbContext {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.UserName).IsUnique();

        }

        public DevBlogContext() : base("Developer Blog"){

        }

        DbSet<UserEntity> Users { get; set; }
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<PostEntity> Posts { get; set; }
        DbSet<ImageEntity> Images { get; set; }
    }
}

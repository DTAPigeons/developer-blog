using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess {
    class DevBlogContext: DbContext {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.UserName).IsUnique();

        }

        public DevBlogContext() : base(){

        }

        DbSet<UserEntity> Users { get; set; }
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<PostEntity> Posts { get; set; }
        DbSet<ImageEntity> Images { get; set; }
    }
}

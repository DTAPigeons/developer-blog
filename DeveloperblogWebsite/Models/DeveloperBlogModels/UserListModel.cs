using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class UserListModel : BaseModel<UserEntity> {
        [StringLength(100)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }


        public UserListModel() {

        }

        public UserListModel(UserEntity entity):base(entity) {
            UserName = entity.UserName;
            Role = entity.Role;

        }

        public override UserEntity ToEntity() {
            UserEntity entity = new UserEntity();
            entity.ID = ID;
            entity.UserName = UserName;
            entity.Role = Role;
            return entity;
        }

        public override bool IsValid() {
            return true;
        }
    }
}
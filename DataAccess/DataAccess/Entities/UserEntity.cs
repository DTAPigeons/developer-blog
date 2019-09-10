﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
   public class UserEntity : BaseEntity<UserEntity> {
        [StringLength(100)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }

        public virtual List<PostEntity> Posts { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }

        public override void Update(UserEntity toCopy) {
            UserName = toCopy.UserName;
            Role = toCopy.Role;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
    class UserEntity : BaseEntity {
        [StringLength(100)]
        public string UserName { get; set; }
        public string Role { get; set; }

        public virtual List<PostEntity> Posts { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }
    }
}

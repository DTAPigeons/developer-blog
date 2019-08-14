﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
    class PostEntity: BaseEntity {
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public DateTime TimePosted { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [MaxLength]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public int Views { get; set; }

        public virtual UserEntity Author { get; set; }
        public virtual List<ImageEntity> Images { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }
    }
}

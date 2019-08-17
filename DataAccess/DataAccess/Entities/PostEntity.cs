using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
   public class PostEntity: BaseEntity {
        [ForeignKey("Author")]
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public DateTime TimePosted { get; set; }
        [StringLength(256)]
        [Required]
        public string Title { get; set; }
        [MaxLength]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }

        public virtual UserEntity Author { get; set; }
        public virtual List<ImageEntity> Images { get; set; }
        public virtual List<CommentEntity> Comments { get; set; }
    }
}

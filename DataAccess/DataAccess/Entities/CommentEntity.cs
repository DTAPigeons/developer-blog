using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
   public class CommentEntity: BaseEntity {
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        [MaxLength]
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }
        [ForeignKey("ResponseTo")]
        public int? ResponseToID { get; set; }

        public virtual UserEntity Author { get; set; }
        public virtual List<CommentEntity> Responses { get; set; }
        public virtual CommentEntity ResponseTo { get; set; }
        public virtual PostEntity Post { get; set; }
    }
}

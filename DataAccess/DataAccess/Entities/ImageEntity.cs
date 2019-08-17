using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
   public class ImageEntity: BaseEntity {
        [ForeignKey("Post")]
        [Required]
        public int PostID { get; set; }
        [Required]
        public string Path { get; set; }

        public virtual PostEntity Post { get; set; }
    }
}

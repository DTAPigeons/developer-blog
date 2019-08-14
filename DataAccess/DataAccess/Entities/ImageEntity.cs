using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
    class ImageEntity: BaseEntity {
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public string Path { get; set; }

        public virtual PostEntity Post { get; set; }
    }
}

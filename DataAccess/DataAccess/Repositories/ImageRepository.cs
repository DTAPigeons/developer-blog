using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
   public class ImageRepository : BaseRepository<ImageEntity> {
        public override List<ImageEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "") {
            throw new NotImplementedException();
        }
    }
}

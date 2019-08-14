using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
    class CommentRepository : BaseRepository<CommentEntity> {
        public override List<CommentEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "") {
            throw new NotImplementedException();
        }
    }
}

using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
   public class PostRepository : BaseRepository<PostEntity> {

        public PostRepository() { }

        public override List<PostEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "") {
            List<PostEntity> posts = new List<PostEntity>();
            switch (sortParameter) {
                case "Author":
                    return base.GetAll<int>(pageNumber, pageSize, obj => obj.AuthorID, a => true, descending);
                case "Date":
                    return base.GetAll<DateTime>(pageNumber, pageSize, obj => obj.TimePosted, a => true, descending);
                case "Title":
                    return base.GetAll<string>(pageNumber, pageSize, obj => obj.Title, a => true, descending);
                case "Views":
                    return base.GetAll<int>(pageNumber, pageSize, obj => obj.Views, a => true, descending);
                default:
                    return base.GetAll<int>(pageNumber, pageSize, obj => obj.ID, a => true, descending);
            }
        }

        public List<PostEntity> GetPostsMadeByAuthor(int pageNumber, int pageSize, int authorID) {
            return base.GetAll<int>(pageNumber, pageSize, obj => obj.ID, obj => obj.AuthorID == authorID);
        }
    }
}

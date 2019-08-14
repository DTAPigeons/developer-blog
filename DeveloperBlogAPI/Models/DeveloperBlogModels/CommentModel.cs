using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class CommentModel : BaseModel<CommentEntity> {

        public int AuthorID { get; set; }
        public int PostID { get; set; }
        public string Content { get; set; }
        public DateTime TimePosted { get; set; }
        public int? ResponseToID { get; set; }

        public List<CommentModel> Responses { get; set; }
        public virtual UserModel Author { get; set; }

        public CommentModel() {

        }

        public CommentModel(CommentEntity entity):base(entity) {
            AuthorID = entity.AuthorID;
            PostID = entity.PostID;
            Content = entity.Content;
            TimePosted = entity.TimePosted;
            ResponseToID = entity.ResponseToID;

            Author = new UserModel(entity.Author);

            Responses = new List<CommentModel>();

            foreach(CommentEntity responce in entity.Responses) {
                Responses.Add(new CommentModel(responce));
            }
        }

        public override CommentEntity ToEntity() {
            CommentEntity entity = new CommentEntity();
            entity.ID = ID;
            entity.AuthorID = AuthorID;
            entity.PostID = PostID;
            entity.Content = Content;
            entity.TimePosted = TimePosted;
            entity.ResponseToID = ResponseToID;
            return entity;
        }
    }
}
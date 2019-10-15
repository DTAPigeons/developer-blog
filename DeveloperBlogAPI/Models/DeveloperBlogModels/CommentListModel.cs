using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class CommentListModel : BaseModel<CommentEntity> {
        [Required]
        public int AuthorID { get; set; }
        public string Author { get; set; }
        [Required]
        public int PostID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime TimePosted { get; set; }
        public int? ResponseToID { get; set; }

        public List<CommentListModel> Responses { get; set; }

        public CommentListModel() {

        }

        public CommentListModel(CommentEntity entity):base(entity) {
            AuthorID = entity.AuthorID;
            PostID = entity.PostID;
            Content = entity.Content;
            TimePosted = entity.TimePosted;
            ResponseToID = entity.ResponseToID;

            Author = entity.Author.UserName;

            Responses = new List<CommentListModel>();

            foreach(CommentEntity responce in entity.Responses) {
                Responses.Add(new CommentListModel(responce));
            }
        }

        public override CommentEntity ToEntity() {
            CommentEntity entity = base.ToEntity();
            entity.AuthorID = AuthorID;
            entity.PostID = PostID;
            entity.Content = Content;
            entity.TimePosted = TimePosted;
            entity.ResponseToID = ResponseToID;
            return entity;
        }

        public override bool IsValid() {
            PostRepository postRepositoy = new PostRepository();
            UserRepository userRepository = new UserRepository();
            CommentRepository commentRepository = new CommentRepository();
            PostEntity post = postRepositoy.GetByID(PostID);
            if (post == null) return false;
            if (post.Views <= 0) return false;
            if (userRepository.GetByID(AuthorID) == null) return false;
            if(ResponseToID!=null && ResponseToID > 0) {
                if (commentRepository.GetByID((int)ResponseToID) == null) return false; 
            }
            return true;
        }
    }
}
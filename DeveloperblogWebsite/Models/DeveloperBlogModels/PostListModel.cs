using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class PostListModel : BaseModel<PostEntity> {
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public DateTime TimePosted { get; set; }
        [StringLength(256)]
        [Required]
        public string Title { get; set; }
        [MaxLength]
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }

        public PostListModel() {

        }

        public PostListModel(PostEntity entity):base(entity) {
            AuthorID = entity.AuthorID;
            TimePosted = entity.TimePosted;
            Title = entity.Title;
            Content = entity.Content;
            Views = entity.Views;
        }

        public override PostEntity ToEntity() {
            PostEntity entity = new PostEntity();
            entity.ID = ID;
            entity.AuthorID = AuthorID;
            entity.TimePosted = TimePosted;
            entity.Title = Title;
            entity.Content = Content;
            entity.Views = Views;
            return entity;
        }

        public override bool IsValid() {
            UserRepository userRepository = new UserRepository();
            UserEntity user = userRepository.GetByID(AuthorID);
            return user != null;
        }
    }
}
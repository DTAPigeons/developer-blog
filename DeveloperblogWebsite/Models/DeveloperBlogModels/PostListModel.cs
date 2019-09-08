using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class PostListModel : BaseModel<PostEntity> {
        public string Author { get; set; }
        public DateTime TimePosted { get; set; }
        [StringLength(256)]
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }

        public PostListModel() {

        }
        /*
        public PostListModel(PostEntity entity):base(entity) {
            Author = entity.Author.UserName;
            TimePosted = entity.TimePosted;
            Title = entity.Title;
            Content = entity.Content;
            Views = entity.Views;
        }

        public override PostEntity ToEntity() {
            PostEntity entity = new PostEntity();
            entity.ID = ID;
            entity.TimePosted = TimePosted;
            entity.Title = Title;
            entity.Content = Content;
            entity.Views = Views;
            return entity;
        }
        */
        public override bool IsValid() {
            UserRepository userRepository = new UserRepository();
            return userRepository.UserNameExists(Author);
        }
    }
}
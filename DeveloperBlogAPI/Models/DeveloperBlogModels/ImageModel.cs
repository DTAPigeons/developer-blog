using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class ImageModel : BaseModel<ImageEntity> {
        [Required]
        public int PostID { get; set; }
        [Required]
        public string Path { get; set; }

        public ImageModel() {

        }

        public ImageModel(ImageEntity entity):base(entity) {
            PostID = entity.PostID;
            Path = entity.Path;
        }

        public override ImageEntity ToEntity() {
            ImageEntity entity = new ImageEntity();
            entity.ID = ID;
            entity.PostID = PostID;
            entity.Path = Path;
            return entity;
        }

        public override bool IsValid() {
            PostRepository postRepositoy = new PostRepository();
            PostEntity post = postRepositoy.GetByID(PostID);
            return post != null;
        }
    }
}
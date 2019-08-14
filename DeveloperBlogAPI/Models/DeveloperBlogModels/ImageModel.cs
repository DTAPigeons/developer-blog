using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class ImageModel : BaseModel<ImageEntity> {
        public int PostID { get; set; }
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
    }
}
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class ImageModel : BaseModel<ImageEntity> {
        [Required]
        public int PostID { get; set; }
        public string Path { get; set; }

        public ImageModel() {

        }

        [Required(ErrorMessage = "Please choose an image to upload.")]
        public HttpPostedFileBase ImageFile { get; set; }

        public override bool IsValid() {

            if (ImageFile.ContentType.Contains("image")) {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".bpm", ".png", ".gif", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formats.Any(item => ImageFile.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
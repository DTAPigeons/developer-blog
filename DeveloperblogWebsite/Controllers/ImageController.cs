﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeveloperblogWebsite.Models.DeveloperBlogModels;
using System.Globalization;
using System.Net.Http;
using DeveloperblogWebsite.Helpers;
using System.Threading.Tasks;

namespace DeveloperblogWebsite.Controllers
{
    public class ImageController : Controller
    {
        private readonly string IMAGE_URL = "/Image";

        [HttpPost]
        public async Task<ActionResult> Upload(ImageModel model) {
            if (ModelState.IsValid && model.IsValid()) {

                var requestContent = new MultipartFormDataContent();
                byte[] image = new byte[model.ImageFile.ContentLength + 1];
                model.ImageFile.InputStream.Read(image, 0, image.Length);

                var imageContent = new ByteArrayContent(image);
                requestContent.Add(imageContent, "image", model.ImageFile.FileName);
                requestContent.Headers.ContentDisposition = 
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = model.ImageFile.FileName };

                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(IMAGE_URL + "/Save/" + model.PostID,requestContent, " ");

                if (!responseMessage.IsSuccessStatusCode) { }

                return RedirectToAction("Details", "Post", new { ID = model.PostID });
            }
            else { return RedirectToAction("Details", "Post", new { ID = model.PostID }); }
        }
    }
}
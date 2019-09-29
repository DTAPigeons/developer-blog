using DataAccess.Repositories;
using DeveloperBlogAPI.Messages;
using DeveloperBlogAPI.Models.DeveloperBlogModels;
using DeveloperBlogAPI.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Image")]
    public class ImageController : ApiController
    {
        private readonly string IMAGES_FILE = "~/PostImages";

        ImageRepository repository = new ImageRepository();

        private static readonly SemaphoreSlim imageLock = new SemaphoreSlim(1,1);

        [HttpPost]
        [Route("Save/{postId}")]
        public async Task<IHttpActionResult> Save(int postId) {
            var request = HttpContext.Current.Request;
            ResponseMessage response = new ResponseMessage();

            if (!Request.Content.IsMimeMultipartContent()) {
                response = new ResponseMessage() {
                    Code = HttpStatusCode.UnsupportedMediaType,
                    Body = "Please Upload a valid image"
                };
            }

            await imageLock.WaitAsync();
            try {
              
                string rootPath = HttpContext.Current.Server.MapPath(IMAGES_FILE);
                var provider = new ImageMultipartFormDataStreamProvider(rootPath);
                await Request.Content.ReadAsMultipartAsync(provider);
                    ImageModel model = new ImageModel();
                    var fileContent = provider.Contents.SingleOrDefault();
                    if (provider.LastGeneratedName == null || provider.LastGeneratedName == "") {
                        throw new Exception("Trying to save empty file name!");
                    }
                    var filePath = Path.Combine(rootPath, provider.LastGeneratedName);

                    model.Path = filePath;
                    model.PostID = postId;

                    repository.Save(model.ToEntity());
            }
            catch(Exception ex) {
                response = new ResponseMessage() {
                    Code = HttpStatusCode.InternalServerError,
                    Body = "Save failed!: " + ex.Message
                };
            }
            finally {
                imageLock.Release();
            }

            return Json(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(int id) {
            ImageModel model = null;
            await imageLock.WaitAsync();
            try {
                model = new ImageModel(repository.GetByID(id));
                if (model == null) {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                using (var stream = new FileStream(model.Path, FileMode.Open)) {
                    int length = Convert.ToInt32(stream.Length);
                    byte[] image = new byte[length];
                    await stream.ReadAsync(image, 0, length);
                    var result = new HttpResponseMessage(HttpStatusCode.OK) {
                        Content = new ByteArrayContent(image)
                    };
                    result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") {
                        FileName = stream.Name
                    };
                    return result;
                }

            }
            catch(Exception ex) {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            finally {
                imageLock.Release();
            }
        }
    }
}

using DataAccess.Repositories;
using DeveloperBlogAPI.Messages;
using DeveloperBlogAPI.Models.DeveloperBlogModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DeveloperBlogAPI.Controllers
{
    [AllowAnonymous]
    public class ImageController : ApiController
    {
        ImageRepository repository = new ImageRepository()


        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save() {
            var request = HttpContext.Current.Request;
            Dictionary<string, ResponseMessage> responses = new Dictionary<string, Messages.ResponseMessage>();
            IList<string> allowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
            int maxContentLength = 1024 * 1024 * 5; //Size = 5 MB

            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            PostListModel post = JsonConvert.DeserializeObject<PostListModel>(jsonContent);

            foreach (string file in request.Files) {
                var postedFile = request.Files[file];
                if (postedFile != null && postedFile.ContentLength > 0) {
                    string ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    string extension = ext.ToLower();

                    if (!allowedFileExtensions.Contains(extension)) {
                        ResponseMessage message = new ResponseMessage() {
                            Code = HttpStatusCode.InternalServerError,
                            Body = "Please Upload image of type .jpg,.gif,.png."
                        };
                        responses.Add("error", message);
                    }
                    else if (postedFile.ContentLength > maxContentLength) {
                        ResponseMessage message = new ResponseMessage() {
                            Code = HttpStatusCode.InternalServerError,
                            Body = "Please Upload a file upto " +maxContentLength+ " mb."
                        };
                        responses.Add("error", message);
                    }
                    else {
                        ImageModel model = new ImageModel();
                        var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + post.ID + DateTime.Now.Millisecond + extension);
                        postedFile.SaveAs(filePath);
                        model.Path = filePath;
                        model.PostID = post.ID;
                        try {
                            repository.Save(model.ToEntity());
                            ResponseMessage message = new ResponseMessage() {
                                Code = HttpStatusCode.Accepted,
                                Body = "Upload successfull" + maxContentLength + " mb."
                            };
                            responses.Add("succes", message);
                        }
                        catch(Exception ex) { 
                            ResponseMessage message = new ResponseMessage() {
                                Code = HttpStatusCode.InternalServerError,
                                Body = "Save failed!: " + ex.Message
                            };
                            responses.Add("error", message);
                        }
                    }
                }
                else {
                    ResponseMessage message = new ResponseMessage() {
                        Code = HttpStatusCode.InternalServerError,
                        Body = "Please upload a file!" 
                    };
                    responses.Add("error", message);
                }
            }
            return Json(responses);
        }
    }
}

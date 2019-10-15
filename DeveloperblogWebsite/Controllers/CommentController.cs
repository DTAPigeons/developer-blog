using DeveloperblogWebsite.Helpers;
using DeveloperblogWebsite.Models.DeveloperBlogModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeveloperblogWebsite.Controllers
{
    public class CommentController : Controller
    {
        private readonly string COMMENT_URL = "/Comment";

        [HttpPost]
        public async Task<ActionResult> Create(CommentInsertModel model) {
            model.Author = "DeathToAllPigeons";
            if (ModelState.IsValid) {
                var content = JsonConvert.SerializeObject(model, Formatting.Indented);
                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(COMMENT_URL + "/Save", new StringContent(content), " ");
            }

            return RedirectToAction("Details", "Post", new { ID = model.PostID });
        }

        public async Task<ActionResult> Edit(int id) {
            try {
                CommentInsertModel comment;
                HttpResponseMessage responseMessage = await HttpHelper.GetResponsetMassage(COMMENT_URL + "/" + id, "");
                if (responseMessage.IsSuccessStatusCode) {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    comment = JsonConvert.DeserializeObject<CommentInsertModel>(responseData);
                    return View(comment);
                }
                return HttpNotFound();
            }
            catch (Exception) {
                return HttpNotFound();
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(CommentInsertModel model) {
            if (ModelState.IsValid) {
                var content = JsonConvert.SerializeObject(model, Formatting.Indented);
                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(COMMENT_URL + "/Save", new StringContent(content), "");
                if (responseMessage.IsSuccessStatusCode) {
                    return RedirectToAction("Details", "Post", new { ID = model.PostID });
                }
                else {
                    return View(model);
                }
            }
            else {
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id, int postId) {
            HttpResponseMessage responseMessage = await HttpHelper.DeleteResponsetMassage(COMMENT_URL + "/Delete/" + id, "");
            if (responseMessage.IsSuccessStatusCode) {
                return RedirectToAction("Details", "Post", new { ID = postId }) ;
            }
            else return HttpNotFound();
        }

        public async Task<ActionResult> Respond(int responseToID) {
            CommentListModel responceTo;
            HttpResponseMessage responseMessage = await HttpHelper.GetResponsetMassage(COMMENT_URL + "/" + responseToID, "");
            if (responseMessage.IsSuccessStatusCode) {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                responceTo = JsonConvert.DeserializeObject<CommentListModel>(responseData);
                CommentResponceModel responce = new CommentResponceModel() { ResponceTo = responceTo, PostID = responceTo.PostID };
                if(responceTo.ResponseToID!=null && responceTo.ResponseToID > 0) {
                    responce.ResponseToID = responceTo.ResponseToID;
                }
                else {
                    responce.ResponseToID = responceTo.ID;
                }

                return View(responce);
            }
            else {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Respond(CommentResponceModel responce) {
            responce.Author = "DeathToAllPigeons";
            if (ModelState.IsValid) {
                var content = JsonConvert.SerializeObject(responce, Formatting.Indented);
                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(COMMENT_URL + "/Save", new StringContent(content), " ");
            }

            return RedirectToAction("Details", "Post", new { ID = responce.PostID });
        }
    }
}
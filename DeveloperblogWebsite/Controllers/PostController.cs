using DeveloperblogWebsite.Helpers;
using DeveloperblogWebsite.Models.DeveloperBlogModels;
using DeveloperblogWebsite.Models.DeveloperBlogModels.PostModels;
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
    public class PostController : Controller
    {
        private readonly string POST_URL = "/Post";

        // GET: Post
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10, bool descending = true, string sortParameter = "Date") {
            PagerModel pager = new PagerModel() { Page = page, PageSize = pageSize, Desending = descending, SortParameter = sortParameter };
            var content = JsonConvert.SerializeObject(pager, Formatting.Indented);
            List<PostListedModel> posts = new List<PostListedModel>();
            HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(POST_URL, new StringContent(content),"");
            if (responseMessage.IsSuccessStatusCode) {
               
                var responceData = responseMessage.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<PostListedModel>>(responceData);
                
            }
            HttpResponseMessage postsCountResponseMessage = await HttpHelper.GetResponsetMassage(POST_URL + "/Count");

            if (postsCountResponseMessage.IsSuccessStatusCode) {
                var responceData = postsCountResponseMessage.Content.ReadAsStringAsync().Result;
                var entityCountData = JsonConvert.DeserializeAnonymousType(responceData, new { entityCount = 0 });
                pager.PagesCount = (int)Math.Ceiling(entityCountData.entityCount / (double)pageSize);
            }
            return View(new PostListingModel(pager,posts));
        }

        public ActionResult Create() {
            return View();
        }

        public async Task<ActionResult> Details(int id) {
            HttpResponseMessage responseMessage = await HttpHelper.GetResponsetMassage(POST_URL +"/"+ id, "");
            if (responseMessage.IsSuccessStatusCode) {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                PostViewModel post = JsonConvert.DeserializeObject<PostViewModel>(responseData);
                post.CommentInsert = new CommentInsertModel() { PostID = post.ID};                
                return View(post);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PostInsertModel model) {
            model.Author = "DeathToAllPigeons";
            if (ModelState.IsValid) {
                var content = JsonConvert.SerializeObject(model, Formatting.Indented);
                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(POST_URL+"/Save", new StringContent(content), " ");
                if (responseMessage.IsSuccessStatusCode) {
                    return RedirectToAction("Index");
                }
                else {
                    return View();
                }
            }
            else {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id) {
            try {
                PostInsertModel post;
                HttpResponseMessage responseMessage = await HttpHelper.GetResponsetMassage(POST_URL + "/" + id, "");
                if (responseMessage.IsSuccessStatusCode) {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    post = JsonConvert.DeserializeObject<PostInsertModel>(responseData);
                    return View(post);
                }
                return HttpNotFound();
            }
            catch (Exception) {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PostInsertModel model) {
            if (ModelState.IsValid) {
                var content = JsonConvert.SerializeObject(model, Formatting.Indented);
                HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(POST_URL+"/Save", new StringContent(content), "");
                if (responseMessage.IsSuccessStatusCode) {
                    return RedirectToAction("Details", new { id=model.ID});
                }
                else {
                    return View(model);
                }
            }
            else {
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id) {
            HttpResponseMessage responseMessage = await HttpHelper.DeleteResponsetMassage(POST_URL + "/Delete/" + id, "");
            if (responseMessage.IsSuccessStatusCode) {
                return RedirectToAction("Index");
            }
            else return HttpNotFound();
        }
    }
}
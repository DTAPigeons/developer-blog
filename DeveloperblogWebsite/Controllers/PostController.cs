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
    public class PostController : Controller
    {
        private readonly string POST_URL = "/Post";
        private readonly int PAGE_SIZE = 10;

        // GET: Post
        public async Task<ActionResult> Index(PostListModel model, int page = 1, bool descending = true, string sortParameter = "Date") {
            PagerModel pager = new PagerModel() { Page = page, PageSize = PAGE_SIZE, Desending = descending, SortParameter = sortParameter };
            var content = JsonConvert.SerializeObject(pager, Formatting.Indented);
            List<PostListModel> posts = new List<PostListModel>();
            HttpResponseMessage responseMessage = await HttpHelper.PostResponsetMassage(POST_URL, new StringContent(content),"");
            if (responseMessage.IsSuccessStatusCode) {
               
                var responceDate = responseMessage.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<PostListModel>>(responceDate);
                
            }
            return View(posts);
        }

        public ActionResult Create() {
            return View();
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
    }
}
using DeveloperblogWebsite.Helpers;
using DeveloperblogWebsite.Models.DeveloperBlogModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DeveloperblogWebsite.Controllers
{
    public class PostController : Controller
    {
        private readonly string POST_URL = "/Post";
        private readonly int PAGE_SIZE = 10;

        // GET: Post
        public async System.Threading.Tasks.Task<ActionResult> Index(PostListModel model, int page = 1, bool descending = true, string sortParameter = "Date") {
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
    }
}
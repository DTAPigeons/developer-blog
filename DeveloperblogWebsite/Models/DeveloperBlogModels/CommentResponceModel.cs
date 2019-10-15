using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public class CommentResponceModel: CommentInsertModel{
        public CommentListModel ResponceTo { get; set; }
    }
}
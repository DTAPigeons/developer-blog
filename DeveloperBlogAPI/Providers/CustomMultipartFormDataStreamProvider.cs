using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DeveloperBlogAPI.Providers {
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers) {
            string fileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            int typeSeperator = fileName.LastIndexOf(".");
            string type = fileName.Substring(typeSeperator);
            return base.GetLocalFileName(headers) + type;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace DeveloperBlogAPI.Providers {
    public class ImageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider {
        public ImageMultipartFormDataStreamProvider(string path) : base(path) { }

        private readonly string[] allowedFileExtensions = { ".jpg", ".png",".gif"};

        private string lastGeneratedName;

        public override string GetLocalFileName(HttpContentHeaders headers) {
            string fileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            int typeSeperator = fileName.LastIndexOf(".");
            string type = fileName.Substring(typeSeperator).ToLower();
            if (!allowedFileExtensions.Contains(type)) {
                throw new Exception("Please Upload image of type .jpg,.gif,.png.");
            }
            lastGeneratedName = base.GetLocalFileName(headers) + type;
            return lastGeneratedName;
        }

        public string LastGeneratedName {
            get {
                return lastGeneratedName;
            }
        }

    }
}
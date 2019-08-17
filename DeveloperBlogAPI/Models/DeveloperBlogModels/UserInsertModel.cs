using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public class UserInsertModel:UserListModel {
        public override bool IsValid() {
            UserRepository repository = new UserRepository();
            return repository.UserNameExists(UserName);
        }
    }
}
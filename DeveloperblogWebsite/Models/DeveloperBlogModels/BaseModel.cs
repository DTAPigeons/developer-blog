using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperblogWebsite.Models.DeveloperBlogModels {
    public abstract class BaseModel<TEntity> where TEntity:BaseEntity<TEntity> {
        public int ID { get; set; }

        public BaseModel() {

        }

        /*
        public BaseModel(TEntity entity) {
            ID = entity.ID;
        }
        */
      //  public abstract TEntity ToEntity();

        public abstract bool IsValid();
    }
}
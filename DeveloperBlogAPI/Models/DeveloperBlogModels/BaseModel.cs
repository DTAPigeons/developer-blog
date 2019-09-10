using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperBlogAPI.Models.DeveloperBlogModels {
    public abstract class BaseModel<TEntity> where TEntity:BaseEntity<TEntity> {
        public int ID { get; set; }

        public BaseModel() {

        }

        public BaseModel(TEntity entity) {
            ID = entity.ID;
        }

        public virtual TEntity ToEntity() {
            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
            entity.ID = ID;
            return entity;
        }

        public abstract bool IsValid();
    }
}
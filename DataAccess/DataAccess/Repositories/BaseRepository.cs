using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
    public abstract class BaseRepository<TEntity> where TEntity:BaseEntity<TEntity> {
        protected DevBlogContext context;
        protected DbSet<TEntity> entities;

        public BaseRepository() {
            context = new DevBlogContext();
            entities = context.Set<TEntity>();
        }

        public int GetFirstID() {
                entities = context.Set<TEntity>();
                return entities.First().ID;
        }

        public TEntity GetByID(int id) {
                entities = context.Set<TEntity>();
                return entities.Where(enity => enity.ID == id).FirstOrDefault();       
        }

        public List<TEntity> GetAll() {
                entities = context.Set<TEntity>();
                return entities.ToList();
        }

        public List<TEntity> GetAll(int pageNumber = 1, int pageSize = 100) {
                entities = context.Set<TEntity>();
                return entities.OrderBy(entity => entity.ID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public abstract List<TEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "");

        public List<TEntity> GetAll<TKey>(int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderFilter, Expression<Func<TEntity, bool>> includeFilter, bool descending = false) {
                entities = context.Set<TEntity>();
                if (descending) { return entities.OrderByDescending(orderFilter).Where(includeFilter).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); }
                return entities.OrderBy(orderFilter).Where(includeFilter).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();           
        }

        public int GetCount() {
                entities = context.Set<TEntity>();
                return entities.Count();           
        }

        public void Save(TEntity entity) {
                entities = context.Set<TEntity>();
                var oldEntity = entities.FirstOrDefault(ent => ent.ID == entity.ID);
                if (oldEntity!=null) {
                    oldEntity.Update(entity);
               } 
                           
                else {
                    entities.Add(entity);
                }

                context.SaveChanges();
        }

        public void Delete(TEntity entity) {
            using (context = new DevBlogContext()) {
                entities = context.Set<TEntity>();
                entities.Remove(entity);
                context.SaveChanges(); ;
            }
        }
    }
}

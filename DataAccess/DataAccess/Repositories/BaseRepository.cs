using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
    public abstract class BaseRepository<TEntity> where TEntity:BaseEntity {
        protected DevBlogContext context;
        protected DbSet<TEntity> entities;

        public BaseRepository() {
            context = new DevBlogContext();
            entities = context.Set<TEntity>();
        }

        public int GetFirstID() {
            return entities.First().ID;
        }

        public TEntity GetByID(int id) {
            return entities.Where(enity => enity.ID == id).FirstOrDefault();
        }

        public List<TEntity> GetAll() {
            return entities.ToList();
        }

        public List<TEntity> GetAll(int pageNumber = 1, int pageSize = 100) {
            return entities.OrderBy(entity => entity.ID).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public abstract List<TEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "");

        public List<TEntity> GetAll<TKey>(int pageNumber, int pageSize, Expression<Func<TEntity, TKey>> orderFilter, Expression<Func<TEntity, bool>> includeFilter, bool descending = false) {
            if (descending) { return entities.OrderByDescending(orderFilter).Where(includeFilter).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(); }
            return entities.OrderBy(orderFilter).Where(includeFilter).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetCount() {
            return entities.Count();
        }

        public void Save(TEntity entity) {
            if (entity.ID > 0) {
                Update(entity);
            }
            else {
                entity.ID = entities.Count() + 1;
                Insert(entity);
            }

            context.SaveChanges();
        }

        private void Insert(TEntity entity) {
            entities.Add(entity);
        }

        private void Update(TEntity entity) {
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity) {
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}

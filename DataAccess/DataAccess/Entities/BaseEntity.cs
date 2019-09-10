using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities {
    public abstract class BaseEntity<TEntity> where TEntity : BaseEntity<TEntity> {
        public int ID { get; set; }

        public abstract void Update(TEntity toCopy);
    }
}

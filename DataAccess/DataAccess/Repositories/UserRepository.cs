using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories {
   public class UserRepository:BaseRepository<UserEntity> {
        public override List<UserEntity> GetAll(int pageNumber, int pageSize, bool descending, string sortParameter = "") {
            switch (sortParameter) {
                case "User Name":
                    return base.GetAll<string>(pageNumber, pageSize, obj => obj.UserName, a => true, descending);
                case "Role":
                    return base.GetAll<string>(pageNumber, pageSize, obj => obj.Role, a => true, descending);
                default:
                    return base.GetAll<int>(pageNumber, pageSize, obj => obj.ID, a => true, descending);
            }
        }

        public bool UserNameExists(string userName) {
            return entities.FirstOrDefault(user => user.UserName == userName) != null;
        }

        public int GetIDByUserName(string userName) {
            return entities.First(e => e.UserName == userName).ID;
        }
    }
}

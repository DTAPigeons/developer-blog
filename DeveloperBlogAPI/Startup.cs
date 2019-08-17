using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using DeveloperBlogAPI.Models;
using DataAccess.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccess.Entities;

[assembly: OwinStartup(typeof(DeveloperBlogAPI.Startup))]

namespace DeveloperBlogAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers() {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            UserRepository rep = new UserRepository();

            // In Startup iam creating first Dev Role and creating a default Admin User    
            if (!roleManager.RoleExists("Dev")) {

                // first we create Dev rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Dev";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "DeathToAllPigeons";
                user.Email = "pev97@abv.bg";

                string userPWD = "fuckyou97";

                var chkUser = UserManager.Create(user, userPWD);


                //Add default User to Role Admin   
                if (chkUser.Succeeded) {
                    if (!rep.UserNameExists("DeathToAllPigeons")) {
                        rep.Save(new UserEntity() { UserName = "DeathToAllPigeons", Role = "Dev" });
                    }
                    var result1 = UserManager.AddToRole(user.Id, "Dev");

                }
            }

            // creating Creating Reader role    
            if (!roleManager.RoleExists("Reader")) {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Reader";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Chungus";
                user.Email = "pavkat97@abv.bg";

                string userPWD = "123456";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded) {
                    var result1 = UserManager.AddToRole(user.Id, "Reader");
                    if (!rep.UserNameExists("Chungus")) {
                        rep.Save(new UserEntity() { UserName = "Chungus", Role = "Reader" });
                    }
                }
            }

            // creating Creating Banned role 
            if (!roleManager.RoleExists("Banned")) {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Banned";
                roleManager.Create(role);

            }
        }
    }
}

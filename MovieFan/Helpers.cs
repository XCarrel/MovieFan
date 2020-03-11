using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MovieFan.Models;

namespace MovieFan
{
    public class Helpers
    {
        // To specify the mode in which a detail view must be used 
        public enum ViewModes { Show, Edit, Create };

        public static Users LoggedInUser(moviefanContext db, SignInManager<IdentityUser> signInManager)
        {
            string loguser = signInManager.Context.User.Identity.Name;
            if (loguser != null)
            {
                Users user;
                if (db.Users.Any(u => u.Email == loguser))
                    user = db.Users.First(u => u.Email == loguser);
                else
                {
                    user = new Users();
                    user.Firstname = "Prénom de " + loguser;
                    user.Lastname = "Nom de " + loguser;
                    user.Email = loguser;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return user;
            }
            else
                return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieFan.Models
{
    public partial class Movies
    {
        public List<string> Likers
        {
            get => this.UserLikeMovie.Select(u => u.User.FullName).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate
        {
            get => this.Release;
            set => this.Release = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    public partial class Users
    {
        [NotMapped]
        public bool IsAdministrator
        {
            get => (this.IsAdmin == 1);
            set => this.IsAdmin = (value ? (byte)1 : (byte)0);
        }

        [NotMapped]
        public bool ProfileOk
        {
            get => (this.IsActive == 1);
            set => this.IsActive = (value ? (byte)1 : (byte)0);
        }

        public string FullName
        {
            get => $"{this.Firstname} {this.Lastname}";
        }
    }
}

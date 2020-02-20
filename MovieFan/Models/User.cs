using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieFan.Models
{
    public class User
    {
        private int id;
        private string firstname;
        private string lastname;
        private bool isadmin;

        public User(int id, string firstname, string lastname, bool isadmin)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.isadmin = isadmin;
        }

        public string Firstname { get => this.firstname; }
        public string Lastname { get => this.lastname; }
        public bool IsAdmin { get => this.isadmin; }
        public int Id { get => this.id; } 
    }
}

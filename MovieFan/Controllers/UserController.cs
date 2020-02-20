using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFan.Models;

namespace MovieFan.Controllers
{

    public class UserController : Controller
    {
        private List<User> getusers()
        {
            return new List<User>()
                {
                    new User(1,"Allistair","Cunningham",false),
                    new User(2,"Sarah","Weaver",false),
                    new User(3,"Lucas","Knowles",false),
                    new User(4,"Rinah","Jefferson",false),
                    new User(5,"Timon","Hardy",false),
                    new User(6,"Nell","Marks",false),
                    new User(7,"Elmo","Middleton",false),
                    new User(8,"Jane","Vance",false),
                    new User(9,"Lee","Conway",false),
                    new User(10,"Bevis","Blake",false),
                    new User(11,"Cecilia","Hardin",false),
                    new User(12,"Taylor","Solis",false),
                    new User(13,"Lael","Spence",false),
                    new User(14,"Tyrone","Faulkner",false),
                    new User(15,"Jelani","Jones",false),
                    new User(16,"Jane","Bradshaw",false),
                    new User(17,"Xena","Waters",false),
                    new User(18,"Basil","Roman",false),
                    new User(19,"Bruce","Hammond",true),
                    new User(20,"Ulysses","Stein",false),
                    new User(21,"Uta","Valenzuela",false),
                    new User(22,"Beatrice","Mendoza",false),
                    new User(23,"Chastity","Miller",false),
                    new User(24,"Zenia","Miles",false),
                    new User(25,"Jermaine","Shepherd",false),
                    new User(26,"Adele","Mason",false),
                    new User(27,"Cullen","Bender",false),
                    new User(28,"Macaulay","Tran",false),
                    new User(29,"Timon","Thornton",false),
                    new User(30,"Noble","Goff",false),
                    new User(31,"Aquila","Huffman",false),
                    new User(32,"Dorian","Carlson",false),
                    new User(33,"Iola","Monroe",false),
                    new User(34,"Aiko","Schultz",false),
                    new User(35,"Myles","Osborne",false),
                    new User(36,"Sacha","Ewing",false),
                    new User(37,"Cameran","Ross",false),
                    new User(38,"Beck","Prince",false),
                    new User(39,"Megan","Bass",false),
                    new User(40,"Hyacinth","Sears",false),
                    new User(41,"Aquila","Brady",false),
                    new User(42,"Lee","Erickson",false),
                    new User(43,"Maia","Alexander",false),
                    new User(44,"Elizabeth","Clark",false),
                    new User(45,"Kibo","Camacho",true),
                    new User(46,"Levi","Phillips",false),
                    new User(47,"Kendall","Benson",false),
                    new User(48,"Vladimir","Harris",false),
                    new User(49,"Alika","Waters",false),
                    new User(50,"Hiroko","Sparks",false),
                    new User(51,"Nissim","Chavez",false),
                    new User(52,"Josiah","Leonard",false),
                    new User(53,"Daphne","Holcomb",false),
                    new User(54,"Alma","Aguilar",false),
                    new User(55,"Rae","Rowe",false),
                    new User(56,"Clementine","Nieves",false),
                    new User(57,"Jade","Perez",false),
                    new User(58,"Kasper","Mccarthy",false),
                    new User(59,"Odessa","Salas",false),
                    new User(60,"Zane","Craig",false),
                    new User(61,"Tucker","Bowman",false),
                    new User(62,"Cain","Mcfarland",false),
                    new User(63,"Keefe","Suarez",false),
                    new User(64,"Teagan","Blackwell",false),
                    new User(65,"Melyssa","Morin",false),
                    new User(66,"Taylor","Solo",false),
                    new User(67,"Perry","Tillman",false),
                    new User(68,"Barrett","Monroe",false),
                    new User(69,"Evangeline","Mccormick",false),
                    new User(70,"Kenneth","Hester",false),
                    new User(71,"Gray","Dixon",false),
                    new User(72,"Dane","Solis",false),
                    new User(73,"Quentin","Workman",false),
                    new User(74,"Peter","Cobb",false),
                    new User(75,"Price","Sanford",false),
                    new User(76,"Jana","Adkins",false),
                    new User(77,"Zenia","Baldwin",false),
                    new User(78,"Sara","Fernandez",false),
                    new User(79,"Reagan","Ruiz",false),
                    new User(80,"Kylee","Mcdaniel",false),
                    new User(81,"Urielle","Crawford",true),
                    new User(82,"Shaine","Parker",false),
                    new User(83,"Dean","Jimenez",false),
                    new User(84,"Cullen","Mckay",false),
                    new User(85,"Vladimir","Hale",false),
                    new User(86,"Carter","Estrada",false),
                    new User(87,"Pascale","Morin",false),
                    new User(88,"Sonia","Edwards",false),
                    new User(89,"Xerxes","Hart",false),
                    new User(90,"Jonah","Finch",false),
                    new User(91,"Clare","Barry",false),
                    new User(92,"Harriet","King",false),
                    new User(93,"Garrison","Herrera",false),
                    new User(94,"Justine","Mendoza",false),
                    new User(95,"Harding","Kelley",false),
                    new User(96,"Benjamin","Kirkland",false),
                    new User(97,"Tana","Woods",false),
                    new User(98,"Hector","Marks",false),
                    new User(99,"Garrett","Stephens",false),
                    new User(100,"Rahim","Hobbs",false)
                };
        }
        // GET: User
        public ActionResult Index()
        {
            List<User> users = getusers();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = getusers()[id - 1];
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
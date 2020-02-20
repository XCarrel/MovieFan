using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieFan.Models;

namespace MovieFan.Controllers
{
    public class MovieController : Controller
    {
        List<Movie> movies = new List<Movie>()
            {
                new Movie(1,"Superman vs Batman","Enfant, Bruce Wayne fuit l'enterrement de ses parents (un flash-back montre leur assassinat par un inconnu), puis tombe dans la caverne où une nuée de chauve-souris lui inspire son identité de héros. Des années plus tard, alors que Superman affronte le général Zod (événements vus dans le film Man of Steel) à Métropolis, Bruce tente de sauver des gens dans un bâtiment de Wayne Enterprises sans y parvenir complètement. Héros en activité depuis près de 20 ans à Gotham City sous l'identité de Batman, Wayne est persuadé que l'être surpuissant sera un jour la perte de l'Humanité et qu'il convient de se préparer à l'éliminer"),
                new Movie(2,"Deadpool","Deadpool est un mercenaire défiguré doté d'une capacité surhumaine de guérison accélérée et capable de prouesses physiques. Le personnage est aussi connu sous le surnom de « Mercenaire à la grande bouche » (Merc with a Mouth) en raison de sa tendance à discuter et plaisanter constamment, notamment en cassant le quatrième mur (en parlant à ses lecteurs) pour causer des effets humoristiques et en faisant des gags récurrents"),
                new Movie(3,"Furious 7","Après avoir vaincu Owen Shaw (Luke Evans) et sa bande, et avoir obtenu l’amnistie, Dominic Toretto (Vin Diesel), Brian O’Conner (Paul Walker) et leurs amis sont de retour aux États-Unis pour mener à nouveau une vie de famille tranquille. Brian commence à s’habituer à sa vie de père tandis que Dom tente d’aider Letty (Michelle Rodríguez) à retrouver la mémoire en la ramenant aux Race Wars (« Guerres de courses ») ; cependant, après une altercation avec Hector(Noel Gugliemi), un vieil ami à eux et organisateur du tournoi, elle s’en va"),
                new Movie(4,"PK","Une mission extra-terrestre arrive dans le désert du Rajasthan et dépose l'un des siens en mission de reconnaissance. Celui-ci croise un voleur qui lui dérobe l'unique objet qui lui sert de communication avec les siens.Nu et désemparé, il cherche à le retrouver."),
                new Movie(5,"Gladiator","Maximus Decimus Meridius, général romain renommé, mène une nouvelle fois les légions de l'empereur Marc Aurèle à la victoire en ce jour de bataille en pays germanique. L'empereur, sentant sa fin proche, annonce le soir même en privé à Maximus qu'il souhaite lui laisser le pouvoir à sa mort, pour qu'il puisse le transmettre au Sénat et que Rome devienne à nouveau une République"),
                new Movie(6,"The Hangover","Doug Billings va se marier avec Tracy Garner, la fille d'un riche habitant de Los Angeles. Pour son enterrement de vie de garçon, il souhaite passer une nuit à Las Vegas avec ses amis, Phil et Stu, et avec Alan, le frère de la future mariée. Sid, le père de Tracy, confie à Doug les clés de sa plus belle voiture, une ancienne Mercedes-Benz, tout en exigeant qu'il soit absolument le seul à la conduire"),
                new Movie(7,"3 Idiots","Raju, Farhan et Rancho deviennent amis à l'Imperial College of Engineering, une école d'ingénieurs renommée en Inde. Rancho est souvent en confrontation avec Viru, le directeur de l'école qui accorde beaucoup d'importance à la compétition. Rancho s'oppose à Chatur, un autre étudiant qui apprécie les méthodes de travail du directeur. Le jour où Rancho fait une blague qui humilie Chatur, ce dernier lance le défi de se retrouver dans dix ans pour évaluer celui qui aura le plus de succès"),
                new Movie(8,"Spectre","Lors d’une mission à Mexico, pendant la fête des morts, James Bond exécute les dernières volontés de l'ancienne M tuée à Skyfall en faisant exploser un appartement où se sont réunis plusieurs terroristes qui projettent de faire sauter un stade sportif. Bond prend en chasse le seul qui a survécu à l'explosion, Marco Sciarra, qui tente de s'échapper par hélicoptère. Bond s'agrippe à l'hélicoptère et, après un violent combat, jette par-dessus bord Sciarra après s'être emparé de l'anneau à motif de pieuvre qu'il portait au doigt"),
                new Movie(9,"Batman Begins","Le jeune Bruce Wayne assiste impuissant au meurtre de ses parents.Profondément traumatisé, il grandit obnubilé par un désir de vengeance et voyage aux quatre coins du monde pour étudier la criminologie et les arts martiaux.La Ligue des ombres2, une secte de guerriers ninja dirigée par Ra's al Ghul, se chargera de son entraînement physique. De retour chez lui à Gotham City, le jeune homme se charge de la gestion de Wayne Enterprises dont il est l'héritier.Opérant depuis le sous-sol du manoir familial avec l'aide de son majordome Alfred Pennyworth, Bruce Wayne se lance alors dans la lutte contre le crime sous le nom de Batman"),
                new Movie(10,"The Dark Knight","Batman aborde une phase décisive de sa guerre au crime.Avec l'aide du lieutenant de police Jim Gordon et du nouveau procureur Harvey Dent, il entreprend de démanteler les dernières organisations criminelles qui infestent les rues de la ville. L'association s'avère efficace, mais le trio se heurte bientôt à un nouveau génie du crime qui répand la terreur et le chaos dans Gotham : le Joker. On ne sait pas d'où il vient ni qui il est.Ce criminel possède une intelligence redoutable doublé d'un humour sordide et n'hésite pas à s'attaquer à la pègre locale dans le seul but de semer le chaos"),
            };

        // GET: Movie
        public ActionResult Index()
        {
            return View(movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View(movies[id-1]);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
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

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
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

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
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
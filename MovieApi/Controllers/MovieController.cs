using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly APIContext _context;

        public MovieController(APIContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Find(movie.Id);

                if (movieInDb == null)
                    return new JsonResult(NotFound());

                // Mise à jour des propriétés de l'objet trouvé
                movieInDb.MovieName = movie.MovieName;
                movieInDb.MovieYear = movie.MovieYear;
                movieInDb.MovieDirector = movie.MovieDirector;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(movie));
        }

        [HttpPut]
        public JsonResult Edit(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            if (movie.Id == 0)
            {
                Console.WriteLine("Veuillez ajouter ce film avant de le modifier");
                return new JsonResult(BadRequest("Veuillez ajouter ce film avant de le modifier"));
            }
            else
            {
                var movieInDb = _context.Movies.Find(movie.Id);

                if (movieInDb == null)
                    return new JsonResult(NotFound());

                // Mise à jour des propriétés de l'objet trouvé
                movieInDb.MovieName = movie.MovieName;
                movieInDb.MovieYear = movie.MovieYear;
                movieInDb.MovieDirector = movie.MovieDirector;

                // Attacher l'entité modifiée au contexte et marquer ses propriétés comme modifiées
                _context.Entry(movieInDb).State = EntityState.Modified;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(movie));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Movies.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Movies.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Movies.ToList();
            return new JsonResult(Ok(result));
        }
    }
}
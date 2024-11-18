using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Data;
using System.Linq;

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
        public JsonResult CreateEdit(Movie movies)
        {
            if (movies.Id == 0)
            { 
                _context.Movies.Add(movies);
            }
            else
            {
                var moviesInDb = _context.Movies.Find(movies.Id);

                if (moviesInDb == null)
                    return new JsonResult(NotFound());

                moviesInDb = movies;
            }


            _context.SaveChanges();

            return new JsonResult(Ok(movies));

        }

        [HttpPut]
        public JsonResult Edit(Movie movies)
        {
            if (movies.Id == 0)
            {
                Console.WriteLine("veuillez ajouter ce film avant de le modifier");
            }
            else
            {
                var moviesInDb = _context.Movies.Find(movies.Id);

                if (moviesInDb == null)
                    return new JsonResult(NotFound());

                moviesInDb = movies;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(movies));

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


        [HttpGet("/GetAll")]
        public JsonResult GetAll()
        {
        
            var result = _context.Movies.ToList();

            return new JsonResult(Ok(result));
        }

    }
}
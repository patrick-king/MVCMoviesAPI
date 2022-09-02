using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCMoviesAPI.Models;

namespace MVCMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MvcMovieContext _context;
        private ILoggerFactory _loggerFactory;
        private IConfiguration _configuration;

        public MoviesController(MvcMovieContext context, ILoggerFactory logFactory, IConfiguration config)
        {
            _context = context;
            _loggerFactory = logFactory;
            _configuration = config;
        }

        // GET: api/Movies
        /// <summary>
        /// Retrieves the list of movies
        /// </summary>
        /// <returns>A list of Movies</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("getMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }      

            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        /// <summary>
        /// Gets the detail record of a movie, given an Id
        /// </summary>
        /// <param name="id">The unique Id of the movie</param>
        /// <returns>A Movie Detail</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
          if (_context.Movies == null)
          {
              return NotFound();
          }
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var logger = _loggerFactory.CreateLogger(_configuration.GetValue("Settings/AppName", "MVC"));
                logger.LogTrace("After create in PutMovie");
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                var logger = _loggerFactory.CreateLogger(_configuration.GetValue("Settings/AppName", "MVC"));
                logger.LogError(dbx,null);

                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a new Movie
        /// </summary>
        /// <param name="movie">The Movie to Create</param>
        /// <returns></returns>
        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {

          if (_context.Movies == null)
          {
              return Problem("Entity set 'MvcMovieContext.Movies'  is null.");
          }
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Services;
using Amzaon_DataWarehouse_BackEnd.Services.ServiceImpl;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Amzaon_DataWarehouse_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _imovieService;

        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            this._movieRepository = movieRepository;
            this._imovieService = new MovieServiceImpl(this._movieRepository);
        }

        [HttpGet]
        [Route("/api/getAvailableMovies")]
        public async Task<ActionResult> GetMoviesByName(string movieName = "")
        {

              return Ok(await _imovieService.GetMoviesByMovieName(movieName));
        }
    }
}


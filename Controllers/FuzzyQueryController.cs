using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Services;
using Amzaon_DataWarehouse_BackEnd.Services.ServiceImpl;
using DataWarehouse.IRepositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Amzaon_DataWarehouse_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class FuzzyQueryController : ControllerBase
    {
        private readonly IFuzzyQueryService _iFuzzyQueryService;

        private readonly IMovieRepository _movieRepository;

        private readonly IDirectorRepository _directorRepository;

        private readonly IActorRepository _actorRepository;

        private readonly ICategoryRepository _categoryRepository;

        public FuzzyQueryController(IMovieRepository movieRepository,IDirectorRepository directorRepository,IActorRepository actorRepository,ICategoryRepository categoryRepository)
        {
            this._movieRepository = movieRepository;
            this._directorRepository = directorRepository;
            _actorRepository = actorRepository;
            _categoryRepository = categoryRepository;
            this._iFuzzyQueryService = new FuzzyQueryServiceImpl(this._movieRepository,this._directorRepository,_actorRepository,_categoryRepository);
        }

        [HttpGet]
        [Route("/mysql/association/movie")]
        public async Task<ActionResult> GetMoviesByName(string? movieName = "")
        {
            try
            {
                return Ok(await _iFuzzyQueryService.GetMoviesByMovieName(movieName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet]
        [Route("/mysql/association/director")]
        public async Task<ActionResult> GetDirectorsByName(string? directorName = "")
        {
            try
            {
                return Ok(await _iFuzzyQueryService.GetDirectorsByName(directorName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/mysql/association/actor")]
        public async Task<ActionResult> GetActorsByName(string? actorName = "")
        {
            try
            {
                return Ok(await _iFuzzyQueryService.GetActorsByName(actorName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/mysql/association/category")]
        public async Task<ActionResult> GetCategoriesByName(string? category = "")
        {
            try
            {
                return Ok(await _iFuzzyQueryService.GetCategoriesByName(category));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}


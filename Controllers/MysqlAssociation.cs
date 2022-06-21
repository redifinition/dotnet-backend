using DataWarehouse.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataWarehouse.Services;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Newtonsoft.Json;
using System.Text;
using DataWarehouse.Dtos;

namespace DataWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MysqlAssociation : ControllerBase
    {
        private readonly IMysqlAssociationService mysqlAssociationService;

        private readonly IDirectorRepository _directorRepository;

        private readonly IMovieRepository _movieRepositiry;

        private readonly IActorRepository _actorRepository;
        public MysqlAssociation(IDirectorRepository directorRepository, IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            this._directorRepository = directorRepository;
            _movieRepositiry = movieRepository;
            _actorRepository = actorRepository;
            mysqlAssociationService = new MysqlAssociationServiceImpl(_movieRepositiry, _directorRepository, _actorRepository);
        }

        [HttpGet]
        [Route("/mysql/association/movie/director")]
        public async Task<IActionResult> GetDirectorsByMovieAsin(string movieAsin, int Index)
        {
            try
            {
                object obj = new { index = Index, director = await mysqlAssociationService.GetDirectorNamesByMovieAsin(movieAsin) };

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/movie/mainActor")]
        public async Task<IActionResult> GetMainActorsByMovieAsin(string movieAsin, int Index)
        {
            try
            {
                object obj = new { index = Index, director = await mysqlAssociationService.GetMainActorNamesByMovieAsin(movieAsin) };

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/movie/actor")]
        public async Task<IActionResult> GetActorsByMovieAsin(string movieAsin, int Index)
        {
            try
            {
                object obj = new { index = Index, director = await mysqlAssociationService.GetActorNamesByMovieAsin(movieAsin) };

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/movie/actors")]
        public async Task<IActionResult> GetMoviesByActorAndActor(string actor1, string actor2)
        {
            try
            { 
                return Ok(await mysqlAssociationService.GetMovieNamesByActorAndActor(actor1,actor2));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/movie/actorAndDirector")]
        public async Task<IActionResult> GetMoviesByActorAndDirector(string actorName, string directorName)
        {
            try
            {
                return Ok(await mysqlAssociationService.GetMovieNamesByActorAndDirector(actorName, directorName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/actor/cooperation")]
        public async Task<IActionResult> GetMaxActorCooperationTime()
        {
            try
            {
                return Ok(await mysqlAssociationService.GetMaxCooperationTimeofActors());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/director/cooperation")]
        public async Task<IActionResult> GetMaxDirectorCooperationTime()
        {
            try
            {
                return Ok(await mysqlAssociationService.GetMaxCooperationTimeofDirectors());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/mysql/association/actor/director/cooperation")]
        public async Task<IActionResult> GetMaxActorDirectorCooperationTime()
        {
            try
            {
                return Ok(await mysqlAssociationService.GetMaxCooperationTimeOfActorsDirectors());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Route("/mysql/association/movie/result")]
        public async Task<IActionResult> GetMovieResult(MovieInfoDto movieInfoDto)
        {
            try
            {
                return Ok(await mysqlAssociationService.GetMovieResultsByMutipleRules(movieInfoDto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

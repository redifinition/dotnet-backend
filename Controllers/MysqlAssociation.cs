using DataWarehouse.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataWarehouse.Services;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Newtonsoft.Json;
using System.Text;

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
        public MysqlAssociation(IDirectorRepository directorRepository, IMovieRepository movieRepository,IActorRepository actorRepository)
        {
            this._directorRepository = directorRepository;
            _movieRepositiry = movieRepository;
            _actorRepository = actorRepository;
            mysqlAssociationService = new MysqlAssociationServiceImpl(_movieRepositiry, _directorRepository,_actorRepository);
        }

        [HttpGet]
        [Route("/mysql/association/movie/director")]
        public async Task<IActionResult> GetDirectorsByMovieAsin(string movieAsin,int Index)
        {
            try
            {
                object obj = new {index= Index,director = await mysqlAssociationService.GetDirectorNamesByMovieAsin(movieAsin)};
                
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

    }
}

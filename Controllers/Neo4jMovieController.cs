using DataWarehouse.Dtos;
using DataWarehouse.IRepositories;
using DataWarehouse.Services;
using DataWarehouse.Services.ServiceImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Neo4jMovieController : ControllerBase
    {
        private readonly INeo4jMovieService _neo4jMovieService;

        private readonly INeo4jMovieRepository _neo4jMovieRepository;

        private readonly INeo4jRelationRepository _neo4jRelationRepository;

        public Neo4jMovieController(INeo4jMovieRepository neo4JMovieRepository, INeo4jRelationRepository neo4JRelationRepository)
        {
            _neo4jMovieRepository = neo4JMovieRepository;
            _neo4jRelationRepository = neo4JRelationRepository;
            _neo4jMovieService = new Neo4jMovieServiceImpl(_neo4jMovieRepository, _neo4jRelationRepository);
        }

        [HttpPost]
        [Route("/neo4j/movie")]
        public async Task<IActionResult> GetMovieByCondition(MovieInfoDto movieInfoDto)
        {
            try
            {

                return Ok(await _neo4jMovieService.GetMoviesByConditions(movieInfoDto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/neo4j/movie/name")]
        public async Task<IActionResult> GetMoviesByMovieName(string movieName)
        {
            try
            {

                return Ok(await _neo4jMovieService.GetMoviesByMovieName(movieName));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

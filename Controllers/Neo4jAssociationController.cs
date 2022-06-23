using DataWarehouse.IRepositories;
using DataWarehouse.Services;
using DataWarehouse.Services.ServiceImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Neo4jAssociationController : ControllerBase
    {

        private readonly INeo4jMovieService _neo4jMovieService;

        private readonly INeo4jMovieRepository _neo4jMovieRepository;

        private readonly INeo4jRelationRepository _neo4jRelationRepository;

        public Neo4jAssociationController(INeo4jMovieRepository neo4JMovieRepository, INeo4jRelationRepository neo4JRelationRepository)
        {
            _neo4jMovieRepository = neo4JMovieRepository;
            _neo4jRelationRepository = neo4JRelationRepository;
            _neo4jMovieService = new Neo4jMovieServiceImpl(_neo4jMovieRepository, _neo4jRelationRepository);
        }

        [HttpGet]
        [Route("/neo4j/relation/actorAndDirector")]
        public async Task<IActionResult> GetMostCooperationActorAndDirector()
        {
            try
            {

                return Ok(await _neo4jMovieService.GetMaxCooperationsOfActorAndDirector());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/neo4j/relation/actors")]
        public async Task<IActionResult> GetMostCooperationActors()
        {
            try
            {

                return Ok(await _neo4jMovieService.GetMaxCooperationsOfActors());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

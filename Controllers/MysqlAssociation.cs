using DataWarehouse.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataWarehouse.Services;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Newtonsoft.Json;

namespace DataWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MysqlAssociation : ControllerBase
    {
        private readonly IMysqlAssociationService mysqlAssociationService;

        private readonly IDirectorRepository _directorRepository;

        private readonly IMovieRepository _movieRepositiry;
        public MysqlAssociation(IDirectorRepository directorRepository, IMovieRepository movieRepository)
        {
            this._directorRepository = directorRepository;
            _movieRepositiry = movieRepository;
            mysqlAssociationService = new MysqlAssociationServiceImpl(_movieRepositiry, _directorRepository);
        }

        [HttpGet]
        [Route("/mysql/association/movie/director")]
        public async Task<IActionResult> GetDirectorsByMovieAsin(string movieAsin,int Index)
        {
            try
            {
                object obj = new {index= Index,director = await mysqlAssociationService.GetDirectorNamesByMovieAsin(movieAsin)};
                
                return Ok(JsonConvert.SerializeObject(obj));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

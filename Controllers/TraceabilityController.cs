using DataWarehouse.IRepositories;
using DataWarehouse.Services;
using DataWarehouse.Services.ServiceImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataWarehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraceabilityController : ControllerBase
    {
        private readonly ITraceabilityRepository _traceabilityRepository;

        private readonly ITraceabilityService _traceabilityService;

        public TraceabilityController(ITraceabilityRepository traceabilityRepository)
        {
            _traceabilityRepository = traceabilityRepository;
            _traceabilityService = new TraceabilityServiceImpl(_traceabilityRepository);
        }

        [HttpGet]
        [Route("/traceability/conflictInfo")]
        public async Task<IActionResult> GetMovieVersion(string movieAsin)
        {
            try
            {

                return Ok(await _traceabilityService.GetMovieVersionByAsin(movieAsin));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/comment")]
        public async Task<IActionResult> GetCommentDataList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetCommentListByPage(currentPage,pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/missingAsin")]
        public async Task<IActionResult> GetMissingMovieAsinList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetMissingMovieAsins(currentPage, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/movieTVAsin")]
        public async Task<IActionResult> GetMovieTVAsinList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetMovieTVAsinList(currentPage, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet]
        [Route("/traceability/movie")]
        public async Task<IActionResult> GetMovieList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetMovieList(currentPage, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/consolidationMovie/conflict")]
        public async Task<IActionResult> GetConflictConsolidationMovieList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetConsolidationMovieList(currentPage,pageSize,true));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/consolidationMovie/no-conflict")]
        public async Task<IActionResult> GetConflictConsolidationMovieListWithNoConflict(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetConsolidationMovieList(currentPage, pageSize ,false));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/tvAsin")]
        public async Task<IActionResult> GetTVAsinList(int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.GetTVAsinList(currentPage, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/totalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            try
            {

                return Ok(await _traceabilityService.GetTotalCount());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("/traceability/searchingMovie")]
        public async Task<IActionResult> SearchByTitle(string title, int currentPage, int pageSize)
        {
            try
            {

                return Ok(await _traceabilityService.SearchForTitle(title,currentPage,pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}

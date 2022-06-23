using DataWarehouse.Dtos;

namespace DataWarehouse.Services
{
    public interface INeo4jMovieService
    {
        Task<Dictionary<string, object>> GetMoviesByConditions(MovieInfoDto movieInfoDto);

        Task<Dictionary<string, object>> GetMoviesByMovieName(string movieName);

        Task<Dictionary<string, object>> GetMaxCooperationsOfActorAndDirector();

        Task<Dictionary<string, object>> GetMaxCooperationsOfActors();
    }
}

using DataWarehouse.Dtos;
using Neo4j.Driver;

namespace DataWarehouse.IRepositories
{
    public interface INeo4jMovieRepository
    {
        Task<List<IRecord>> GetMovieByMutipleRules(MovieInfoDto movieInfoDto);

        Task<List<IRecord>> GetMovieByMovieName(string movieName);
    }
}

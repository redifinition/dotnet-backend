using DataWarehouse.Dtos;

namespace DataWarehouse.Services
{
    public interface IMysqlAssociationService
    {
        Task<List<string>?> GetDirectorNamesByMovieAsin(string movieAsin);

        Task<List<string>?> GetMainActorNamesByMovieAsin(string movieAsin);

        Task<List<string>?> GetActorNamesByMovieAsin(string movieAsin);

        Task<List<object>> GetMovieNamesByActorAndActor(string actor1, string actor2);

        Task<List<object>> GetMovieNamesByActorAndDirector(string actorName, string directorName);

        Task<object> GetMaxCooperationTimeofActors();

        Task<object> GetMaxCooperationTimeofDirectors();

        Task<object> GetMaxCooperationTimeOfActorsDirectors();

        Task<Dictionary<string,object>> GetMovieResultsByMutipleRules(MovieInfoDto movieInfoDto);

    }
}

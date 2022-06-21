using Amzaon_DataWarehouse_BackEnd.Models;

namespace DataWarehouse.IRepositories
{
    public interface IActorRepository
    {
        Task<List<ViewActorName>> GetActorsByName(string? actorName);

        Task<List<ActorMovie>> GetActorMoviesByMovieId(int movieId,byte isMainActor);

        Task<List<ViewActorActor>> GetActorActorsByActorName(string actor1, string actor2);

        Task<ViewActorCooperationTime> GetMaxViewActorCooperationTime();

        Task<ViewActorDirectorCooperationTime> GetMaxViewActorDirectorCooperationTime();

    }
}

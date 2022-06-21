using Amzaon_DataWarehouse_BackEnd.Models;

namespace DataWarehouse.IRepositories
{
    public interface IDirectorRepository
    {
        Task<List<ViewDirectorName>> GetDirectorByName(string? directorName);

        Task<List<DirectorMovie>> GetDirectorMoviesByMovieId(int movieId);

        Task<List<ViewActorDirector>> GetActorDirectorByActorAndDirector(string actorName, string directorName);

        Task<ViewDirectorCooperationTime> GetMaxViewDirectorCooperationTime();
    }
}

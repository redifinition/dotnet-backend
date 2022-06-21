using DataWarehouse.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly DataWarehouseContext _datawarhouseContext;
        public DirectorRepository(DataWarehouseContext datawarehoustContext)
        {
            _datawarhouseContext = datawarehoustContext;
        }

        public async Task<List<ViewActorDirector>> GetActorDirectorByActorAndDirector(string actorName, string directorName)
        {
            return await _datawarhouseContext.ViewActorDirectors.Where(s => s.ActorName == actorName && s.DirectorName == directorName).ToListAsync();
        }

        public async Task<List<ViewDirectorName>> GetDirectorByName(string? directorName)
        {
            if (directorName != null)
            {
                return await _datawarhouseContext.ViewDirectorNames.Where(s => s.DirectorName.StartsWith(directorName)).Skip(0).Take(50).ToListAsync();
            }
            else
                return await _datawarhouseContext.ViewDirectorNames.Skip(0).Take(50).ToListAsync();
        }

        public async Task<List<DirectorMovie>> GetDirectorMoviesByMovieId(int movieId)
        {
            return await _datawarhouseContext.DirectorMovies.Where(x => x.MovieId == movieId).ToListAsync();
        }

        public Task<ViewDirectorCooperationTime> GetMaxViewDirectorCooperationTime()
        {
            return _datawarhouseContext.ViewDirectorCooperationTimes.OrderByDescending(s => s.MovieCount).FirstOrDefaultAsync();
        }
    }
}

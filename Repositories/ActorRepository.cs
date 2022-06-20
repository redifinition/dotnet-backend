using DataWarehouse.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Repositories
{
    public class ActorRepository : IActorRepository
    {

        private readonly DataWarehouseContext _datawarhouseContext;
        public ActorRepository(DataWarehouseContext datawarehoustContext)
        {
            this._datawarhouseContext = datawarehoustContext;
        }

        public async Task<List<ActorMovie>> GetActorMoviesByMovieId(int movieId)
        {
            return await _datawarhouseContext.ActorMovies.Where(x => x.MovieId == movieId && x.IsMainActor == 1).ToListAsync();
        }

        public async Task<List<ViewActorName>> GetActorsByName(string? actorName)
        {
            return await _datawarhouseContext.ViewActorNames.Where(s => s.ActorName.StartsWith(actorName)).Skip(0).Take(50).ToListAsync();
        }

    }
}

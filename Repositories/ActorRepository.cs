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

        public async Task<List<ViewActorActor>> GetActorActorsByActorName(string actor1, string actor2)
        {
            return await _datawarhouseContext.ViewActorActors.Where(s => s.Actor1 == actor1 && s.Actor2 == actor2).ToListAsync();
        }

        public async Task<List<ActorMovie>> GetActorMoviesByMovieId(int movieId, byte isMainActor)
        {
            if(isMainActor == 1)
                return await _datawarhouseContext.ActorMovies.Where(x => x.MovieId == movieId && x.IsMainActor == isMainActor).ToListAsync();
            else
                return await _datawarhouseContext.ActorMovies.Where(x => x.MovieId == movieId).ToListAsync();
        }

        public async Task<List<ViewActorName>> GetActorsByName(string? actorName)
        {
            return await _datawarhouseContext.ViewActorNames.Where(s => s.ActorName.StartsWith(actorName)).Skip(0).Take(50).ToListAsync();
        }

        public Task<ViewActorCooperationTime> GetMaxViewActorCooperationTime()
        {
            return _datawarhouseContext.ViewActorCooperationTimes.OrderByDescending(s => s.CooperTime).FirstOrDefaultAsync();
        }

        public Task<ViewActorDirectorCooperationTime> GetMaxViewActorDirectorCooperationTime()
        {
            return _datawarhouseContext.ViewActorDirectorCooperationTimes.OrderByDescending(s => s.MovieCount).FirstOrDefaultAsync();
        }
    }
}

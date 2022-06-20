using Amzaon_DataWarehouse_BackEnd.IRepositories;
using DataWarehouse.IRepositories;

namespace DataWarehouse.Services
{
    public class MysqlAssociationServiceImpl : IMysqlAssociationService
    {
        private readonly IMovieRepository _movieRepositiry;

        private readonly IDirectorRepository _directorRepository;

        private readonly IActorRepository _actorRepository;

        public MysqlAssociationServiceImpl(IMovieRepository movieRepository, IDirectorRepository directorRepository,IActorRepository actorRepository)
        {
            this._movieRepositiry = movieRepository;
            _directorRepository = directorRepository;
            _actorRepository = actorRepository;
        }

        public async Task<List<string>?> GetDirectorNamesByMovieAsin(string movieAsin)
        {
            var movie = await _movieRepositiry.GetMovieByMovieAsin(movieAsin);

            if(movie == null)
                return null;

            var directorMovies = await _directorRepository.GetDirectorMoviesByMovieId(movie.MovieId);
            List<string> results = new List<string>();

            foreach(var director in directorMovies)
            {
                results.Add(director.DirectorName);
            }

            return results;

        }

        public async Task<List<string>?> GetMainActorNamesByMovieAsin(string movieAsin)
        {
            var movie = await _movieRepositiry.GetMovieByMovieAsin(movieAsin);

            if (movie == null)
                return null;

            var actorMovies = await _actorRepository.GetActorMoviesByMovieId(movie.MovieId);
            List<string> results = new List<string>();

            foreach (var actors in actorMovies)
            {
                results.Add(actors.ActorName);
            }

            return results;
        }
    }
}

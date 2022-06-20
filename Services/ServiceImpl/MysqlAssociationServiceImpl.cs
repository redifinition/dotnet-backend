using Amzaon_DataWarehouse_BackEnd.IRepositories;
using DataWarehouse.IRepositories;

namespace DataWarehouse.Services
{
    public class MysqlAssociationServiceImpl : IMysqlAssociationService
    {
        private readonly IMovieRepository _movieRepositiry;

        private readonly IDirectorRepository _directorRepository;

        public MysqlAssociationServiceImpl(IMovieRepository movieRepository, IDirectorRepository directorRepository)
        {
            this._movieRepositiry = movieRepository;
            _directorRepository = directorRepository;
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
    }
}

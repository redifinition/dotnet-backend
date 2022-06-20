using System;
using Amzaon_DataWarehouse_BackEnd.IRepositories;

namespace Amzaon_DataWarehouse_BackEnd.Services.ServiceImpl
{
	public class MovieServiceImpl : IMovieService
	{
		private readonly IMovieRepository _movieRepositiry;

        public MovieServiceImpl(IMovieRepository movieRepository)
        {
            this._movieRepositiry = movieRepository;
        }

        
        public async Task<List<string>> GetMoviesByMovieName(string movieName)
        {
            List<string> movies = new List<string>();
            var results = await _movieRepositiry.GetMoviesByName(movieName);
            foreach(var movie in results)
            {
                movies.Add(movie.MovieName);
            }
            return movies;
        }
    }
}


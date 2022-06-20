using System;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using DataWarehouse.IRepositories;

namespace Amzaon_DataWarehouse_BackEnd.Services.ServiceImpl
{
	public class FuzzyQueryServiceImpl : IFuzzyQueryService
	{
		private readonly IMovieRepository _movieRepositiry;
        private readonly IDirectorRepository _directorRepository;
        private readonly IActorRepository _actorRepository;
        private readonly ICategoryRepository _categoryRepository;

        public FuzzyQueryServiceImpl(IMovieRepository movieRepository, IDirectorRepository directorRepository,IActorRepository actorRepository, ICategoryRepository categoryRepository)
        {
            this._movieRepositiry = movieRepository;
            this._directorRepository = directorRepository;
            this._actorRepository = actorRepository;
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<string>> GetActorsByName(string? actorName)
        {
            List<string> results = new List<string>();
            var actors = await _actorRepository.GetActorsByName(actorName);
            foreach (var actor in actors)
            {
                results.Add(actor.ActorName);
            }
            return results;
        }

        public async Task<List<string>> GetCategoriesByName(string? categoryName)
        {
            List<string> results = new List<string>();
            var categories = await _categoryRepository.GetCategoriesByName(categoryName);
            foreach (var category in categories)
            {
                results.Add(category.CategoryName);
            }
            return results;
        }

        public async Task<List<string>> GetDirectorsByName(string? directorName)
        {
            List<string> directors = new List<string>();
            var results = await _directorRepository.GetDirectorByName(directorName);
            foreach (var director in results)
            {
                directors.Add(director.DirectorName);
            }
            return directors;
        }

        public async Task<List<string>> GetMoviesByMovieName(string? movieName)
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


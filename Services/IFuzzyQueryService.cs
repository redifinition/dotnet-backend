using System;
namespace Amzaon_DataWarehouse_BackEnd.Services
{
	public interface IFuzzyQueryService
	{
		Task<List<string>> GetMoviesByMovieName(string? movieName);

		Task<List<string>> GetDirectorsByName(string? directorName);

		Task<List<string>> GetActorsByName(string? actorName);

		Task<List<string>> GetCategoriesByName(string? categoryName);
	}
}


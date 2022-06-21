using System;
using Amzaon_DataWarehouse_BackEnd.Models;
using DataWarehouse.Dtos;

namespace Amzaon_DataWarehouse_BackEnd.IRepositories
{
	public interface IMovieRepository
	{
		Task<IEnumerable<Movie>> GetMoviesByName(string? movieName);

		Task<Movie> GetMovieByMovieAsin(string movieAsin);

		Task<Movie> GetMovieById(int movieId);

		Task<IEnumerable<int>> GetMovieByMutipleRules(MovieInfoDto movieInfoDto);

		Task<Format?> GetFormatByFormatId(int formatId);

		Task<IEnumerable<MovieResult>> GetMoviesByMovieIds(IEnumerable<int> movieIdList);
	}
}


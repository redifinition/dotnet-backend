using System;
using Amzaon_DataWarehouse_BackEnd.Models;

namespace Amzaon_DataWarehouse_BackEnd.IRepositories
{
	public interface IMovieRepository
	{
		Task<IEnumerable<Movie>> GetMoviesByName(string? movieName);

		Task<Movie> GetMovieByMovieAsin(string movieAsin);
	}
}


using System;
namespace Amzaon_DataWarehouse_BackEnd.Services
{
	public interface IMovieService
	{
		Task<List<string>> GetMoviesByMovieName(string movieName);
	}
}


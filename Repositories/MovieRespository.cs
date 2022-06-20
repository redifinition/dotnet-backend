﻿using System;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Amzaon_DataWarehouse_BackEnd.Repositories
{
	public class MovieRespository:IMovieRepository
	{
        private readonly DataWarehouseContext _datawarhouseContext;
		public MovieRespository(DataWarehouseContext datawarehoustContext)
		{
            _datawarhouseContext = datawarehoustContext;
		}

        public async Task<IEnumerable<Movie>> GetMoviesByName(string movieName)
        {
            return  await _datawarhouseContext.Movies.Where(s => s.MovieName.StartsWith(movieName)).Skip(0).Take(25).ToListAsync();
        }
    }
}

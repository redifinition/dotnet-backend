using System;
using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using DataWarehouse.Dtos;
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


        public async Task<IEnumerable<Movie>> GetMoviesByName(string? movieName)
        {
            if (movieName != null)
                return await _datawarhouseContext.Movies.Where(s => s.MovieName.StartsWith(movieName)).Skip(0).Take(25).ToListAsync();
            else
                return await _datawarhouseContext.Movies.Skip(0).Take(25).ToListAsync();
        }
        public async Task<Movie> GetMovieByMovieAsin(string movieAsin)
        {
            return await _datawarhouseContext.Movies.FirstAsync(s => s.MovieAsin.Equals(movieAsin));
        }

        public async Task<Movie> GetMovieById(int movieId)
        {
            return await _datawarhouseContext.Movies.FirstAsync(s => s.MovieId == movieId);
        }

        public async Task<IEnumerable<int>> GetMovieByMutipleRules(MovieInfoDto movieInfoDto)
        {

            //电影名称查询
            IEnumerable<int> results = new List<int>();
            if(movieInfoDto.movieName != null)
            {
                var movieNameResult = await (from movie in _datawarhouseContext.Movies
                                            where movie.MovieName == movieInfoDto.movieName
                                            select movie.MovieId).ToListAsync();
                results = movieNameResult;
            }
            if (movieInfoDto.minScore != null)
            {
                //评分查询
                var scoreResult = await (from movieScore in _datawarhouseContext.MovieScores
                                         where movieScore.MovieScore1 < Convert.ToSingle(movieInfoDto.maxScore)
                                         && movieScore.MovieScore1 > Convert.ToSingle(movieInfoDto.minScore)
                                         && movieScore.PositiveCommentRating * 100 > movieInfoDto.positive
                                         select movieScore.MovieId).ToListAsync();
                if (movieInfoDto.movieName != null)
                    results = results.Intersect(scoreResult);
                else
                    results = scoreResult;
            }
            //电影类别查询
            if (movieInfoDto.category != null)
            {
                var categoryResult = await (from category in _datawarhouseContext.Categories
                                            where category.CategoryName == movieInfoDto.category
                                            select category.MovieId).ToListAsync();

                    results = results.Intersect(categoryResult);
            }
            //电影导演查询
            if(movieInfoDto.directorNames != null)
            {
                var directorResult = await (from directorMovie in _datawarhouseContext.DirectorMovies
                                            where movieInfoDto.directorNames.Contains(directorMovie.DirectorName)
                                            select directorMovie.MovieId
                                            ).ToListAsync();
                    results = results.Intersect(directorResult);
            }
            //演员查询
            if(movieInfoDto.actors != null)
            {
                var actorResult = await(from actorMovie in _datawarhouseContext.ActorMovies
                                        where movieInfoDto.actors.Contains(actorMovie.ActorName)
                                        && actorMovie.IsMainActor == 0
                                        select actorMovie.MovieId).ToListAsync();
                    results = results.Intersect(actorResult);
            }
            //主演查询
            if (movieInfoDto.mainActors != null)
            {
                var actorResult = await (from actorMovie in _datawarhouseContext.ActorMovies
                                         where movieInfoDto.mainActors.Contains(actorMovie.ActorName)
                                         && actorMovie.IsMainActor == 1
                                         select actorMovie.MovieId).ToListAsync();

                    results = results.Intersect(actorResult);
            }
            //按照日期查询
            if(movieInfoDto.minDate != null)
            {
                var dateResult = await (from time in _datawarhouseContext.Times
                                        join timeMovie in _datawarhouseContext.TimeMovies
                                        on time.TimeStr.ToString() equals timeMovie.TimeStr.ToString()
                                        where time.TimeStr >= movieInfoDto.minDate
                                        && time.TimeStr <= movieInfoDto.maxDate
                                        select timeMovie.MovieId).ToListAsync();
                    results = results.Intersect(dateResult);
            }
            return results;
        }

        public async Task<Format?> GetFormatByFormatId(int formatId)
        {
            return await _datawarhouseContext.Formats.FirstAsync(x => x.FormatId == formatId);
        }

        public async Task<IEnumerable<MovieResult>> GetMoviesByMovieIds(IEnumerable<int> movieIdList)
        {
            return await (from movie in _datawarhouseContext.Movies
                                      join movieFormat in _datawarhouseContext.Formats
                                      on movie.FormatId equals movieFormat.FormatId
                                      into movieFormats
                                      from movie_format in movieFormats.DefaultIfEmpty()
                                      where movieIdList.Contains(movie.MovieId)
                                      select new
                                      MovieResult(movie.MovieAsin,
                                          movie.MovieName,
                                          movie_format.FormatName,
                                          movie.MovieEdition,
                                          movie.MovieScore,
                                          movie.CommentNum,
                                          movie.TimeStr
                                      )).ToListAsync();
        }
    }
}


using Amzaon_DataWarehouse_BackEnd.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using DataWarehouse.Dtos;
using DataWarehouse.IRepositories;
using System.Diagnostics;

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

        public async Task<List<string>?> GetActorNamesByMovieAsin(string movieAsin)
        {
            var movie = await _movieRepositiry.GetMovieByMovieAsin(movieAsin);

            if (movie == null)
                return null;

            var actorMovies = await _actorRepository.GetActorMoviesByMovieId(movie.MovieId, 0);
            List<string> results = new List<string>();

            foreach (var actors in actorMovies)
            {
                results.Add(actors.ActorName);
            }

            return results;
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

            var actorMovies = await _actorRepository.GetActorMoviesByMovieId(movie.MovieId, 1);
            List<string> results = new List<string>();

            foreach (var actors in actorMovies)
            {
                results.Add(actors.ActorName);
            }

            return results;
        }

        public async Task<object> GetMaxCooperationTimeofActors()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start(); 
            var actorCooperationTime = await _actorRepository.GetMaxViewActorCooperationTime();
            stopwatch.Stop();
            var actors = new List<string>();
            actors.Add(actorCooperationTime.ActorName1);
            actors.Add(actorCooperationTime.ActorName2);
            var results = new
            {
                actor = actors,
                number = actorCooperationTime.CooperTime,
                time = stopwatch.ElapsedMilliseconds,
            };
            return results;
        }

        public async Task<List<object>> GetMovieNamesByActorAndActor(string actor1, string actor2)
        {
            var actorActors = await _actorRepository.GetActorActorsByActorName(actor1, actor2);

            var resultList = new List<object>();

            foreach(var actor in actorActors)
            {
                var movie = await _movieRepositiry.GetMovieById(actor.MovieId);

                var obj = new
                {
                    asin = movie.MovieAsin,
                    name = movie.MovieName,
                    score = movie.MovieScore,
                    commentNum = movie.CommentNum
                };
                resultList.Add(obj);
            }
            return resultList;
        }

        public async Task<List<object>> GetMovieNamesByActorAndDirector(string actorName, string directorName)
        {
            var actorDirectors = await _directorRepository.GetActorDirectorByActorAndDirector(actorName, directorName);

            var resultList = new List<object>();

            foreach (var actorDirector in actorDirectors)
            {
                var movie = await _movieRepositiry.GetMovieById(actorDirector.MovieId);

                var obj = new
                {
                    asin = movie.MovieAsin,
                    name = movie.MovieName,
                    score = movie.MovieScore,
                    commentNum = movie.CommentNum
                };
                resultList.Add(obj);
            }
            return resultList;
        }

        public async Task<object> GetMaxCooperationTimeofDirectors()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var DirectorCooperationTime = await _directorRepository.GetMaxViewDirectorCooperationTime();
            stopwatch.Stop();
            var actors = new List<string>();
            actors.Add(DirectorCooperationTime.FirstDirectorName);
            actors.Add(DirectorCooperationTime.SecondDirectorName);
            var results = new
            {
                director = actors,
                number = DirectorCooperationTime.MovieCount,
                time = stopwatch.ElapsedMilliseconds,
            };
            return results;
        }

        public async Task<object> GetMaxCooperationTimeOfActorsDirectors()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var ActorDirectorCooperationTime = await _actorRepository.GetMaxViewActorDirectorCooperationTime();
            stopwatch.Stop();
            var actors = new List<string>();
            actors.Add(ActorDirectorCooperationTime.ActorName);
            actors.Add(ActorDirectorCooperationTime.DirectorName);
            var results = new
            {
                actor = actors[0],
                director = actors[1],
                number = ActorDirectorCooperationTime.MovieCount,
                time = stopwatch.ElapsedMilliseconds,
            };
            return results;
        }

        public async Task<Dictionary<string,object>> GetMovieResultsByMutipleRules(MovieInfoDto movieInfoDto)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var movieIdList = await _movieRepositiry.GetMovieByMutipleRules(movieInfoDto);
            stopwatch.Stop();
            //输出结果
            var movies = new List<Dictionary<string,object>>();
            var movieInfos = await _movieRepositiry.GetMoviesByMovieIds(movieIdList);

            foreach (var movieInfo in movieInfos)
            {
                var movieNode = new Dictionary<string, object>();
                movieNode.Add("asin", movieInfo.asin);
                movieNode.Add("title",movieInfo.title);
                movieNode.Add("format", movieInfo.format);
                movieNode.Add("edition",movieInfo.edition);
                movieNode.Add("score", movieInfo.score);
                movieNode.Add("commentNum", movieInfo.commentNum);
                if(movieInfo.date != null)
                {
                    DateTime dt = (DateTime)movieInfo.date;
                    movieNode.Add("year", dt.Year.ToString());
                    movieNode.Add("month", dt.Month.ToString());
                    movieNode.Add("day", dt.Day.ToString());
                }
                movies.Add(movieNode);
            }
            var results = new Dictionary<string, object>();
            results.Add("movies", movies);
            results.Add("movieNum", movies.Count());
            results.Add("time", stopwatch.ElapsedMilliseconds);
            return results;
        }
    }
}

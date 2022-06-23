using DataWarehouse.Dtos;
using DataWarehouse.IRepositories;
using Neo4j.Driver;
using System.Diagnostics;

namespace DataWarehouse.Services.ServiceImpl
{
    public class Neo4jMovieServiceImpl : INeo4jMovieService
    {
        private readonly INeo4jMovieRepository _neo4jMovieRepository;

        private readonly INeo4jRelationRepository _neo4jRelationRepository;

        public Neo4jMovieServiceImpl(INeo4jMovieRepository neo4JMovieRepository,  INeo4jRelationRepository neo4JRelationRepository)
        {
            _neo4jMovieRepository = neo4JMovieRepository;
            _neo4jRelationRepository = neo4JRelationRepository;
        }

        public async Task<Dictionary<string, object>> GetMaxCooperationsOfActorAndDirector()
        {
            //记录运行时间
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var cooperationTime = await _neo4jRelationRepository.GetMaxCooperationTimesOfActorAndDirector();
            stopWatch.Stop();
            var response = new Dictionary<string, object>();
            response.Add("actor", cooperationTime[0]["p.name"]);
            response.Add("director", cooperationTime[0]["q.name"]);
            response.Add("number", cooperationTime[0]["count(m)"]);
            response.Add("time", stopWatch.ElapsedMilliseconds);
            return response;
        }

        public async Task<Dictionary<string, object>> GetMaxCooperationsOfActors()
        {
            //记录运行时间
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var cooperationTime = await _neo4jRelationRepository.GetMaxCooperationTimesOfActors();
            stopWatch.Stop();
            var response = new Dictionary<string, object>();
            response.Add("actor", cooperationTime[0]["p.name"]);
            response.Add("director", cooperationTime[0]["q.name"]);
            response.Add("number", cooperationTime[0]["count(m)"]);
            response.Add("time", stopWatch.ElapsedMilliseconds);
            return response;
        }

        public async Task<Dictionary<string, object>> GetMoviesByConditions(MovieInfoDto movieInfoDto)
        {
            //记录运行时间
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var movies = await _neo4jMovieRepository.GetMovieByMutipleRules(movieInfoDto);
            stopWatch.Stop();
            int num = 0;
            var movieResult = new List<Dictionary<string, object>>();
            var result = new Dictionary<string, object>();
            foreach (var movie in movies)
            {
                var movieNode = new Dictionary<string,object>();
                if (num < 50)
                {
                    if (movie["m"].As<INode>().Properties.ContainsKey("asin"))
                    {
                        movieNode.Add("asin", movie["m"].As<INode>().Properties["asin"].As<string>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("title"))
                    {
                        movieNode.Add("title", movie["m"].As<INode>().Properties["title"].As<string>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("format"))
                    {
                        movieNode.Add("format", movie["m"].As<INode>().Properties["format"].As<string>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("edition"))
                    {
                        movieNode.Add("edition", movie["m"].As<INode>().Properties["edition"].As<string>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("score"))
                    {
                        movieNode.Add("score", movie["m"].As<INode>().Properties["score"].As<float>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("commentNum"))
                    {
                        movieNode.Add("commentNum", movie["m"].As<INode>().Properties["commentNum"].As<int>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("year"))
                    {
                        movieNode.Add("year", movie["m"].As<INode>().Properties["year"].As<int>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("month"))
                    {
                        movieNode.Add("month", movie["m"].As<INode>().Properties["month"].As<int>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("day"))
                    {
                        movieNode.Add("day", movie["m"].As<INode>().Properties["day"].As<int>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("positive"))
                    {
                        movieNode.Add("positive", movie["m"].As<INode>().Properties["positive"].As<float>());
                    }
                    if (movie["m"].As<INode>().Properties.ContainsKey("negative"))
                    {
                        movieNode.Add("negative", movie["m"].As<INode>().Properties["negative"].As<float>());
                    }
                    num++;
                    movieResult.Add(movieNode);
                }
                else
                    break;
            }
            result.Add("movies", movieResult);
            result.Add("movieNum", movies.Count);
            result.Add("time", stopWatch.ElapsedMilliseconds);
            return result;
        }

        public async Task<Dictionary<string, object>> GetMoviesByMovieName(string movieName)
        {
            //记录运行时间
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var movies = await _neo4jMovieRepository.GetMovieByMovieName(movieName);
            stopWatch.Stop();
            var response = new Dictionary<string, object>();
            var movieList = new List<string>();
            movies.ToList().ForEach(movie => movieList.Add(movie["n"].As<INode>().Properties["title"].ToString()));
            response.Add("movies", movieList);
            response.Add("time", stopWatch.ElapsedMilliseconds);
            return response;
        }
    }
}

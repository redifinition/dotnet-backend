using DataWarehouse.Dtos;
using DataWarehouse.IRepositories;
using DataWarehouse.Models.neo4j;
using Neo4j.Driver;
using System.Diagnostics;
using System.Text;

namespace DataWarehouse.Repositories
{
    public class Neo4jMovieRepository : INeo4jMovieRepository
    {
        private readonly IDriver _driver;

        public Neo4jMovieRepository(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<List<IRecord>> GetMovieByMovieName(string movieName)
        {
            IResultCursor cursor;
            IAsyncSession session = _driver.AsyncSession();
            var movie = new List<IRecord>();
            try
            {
                var statement = "match (n:Movie) where n.title contains('" + movieName + "') return n";
                cursor = await session.RunAsync(statement);
                movie = await cursor.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return movie;
        }

        public  async Task<List<IRecord>> GetMovieByMutipleRules(MovieInfoDto movieInfoDto)
        {
            IResultCursor cursor;
            IAsyncSession session = _driver.AsyncSession();
            var movies = new List<IRecord>();
            try
            {
                var statement = "match (m:Movie)";
                if (movieInfoDto.category != null)
                {
                    statement += ", (m)-[:Belong]->(:Category{name:\"" + movieInfoDto.category + "\"}) ";
                }
                //导演名称
                if (movieInfoDto.directorNames != null)
                {
                    foreach (var directorName in movieInfoDto.directorNames)
                    {
                        statement += " ,(m)<-[:Direct]-(:Person{name\"" + directorName + "\"})";
                    }
                }
                //主演名称
                if (movieInfoDto.mainActors != null)
                {
                    foreach (var mainActor in movieInfoDto.mainActors)
                    {
                        statement += " ,(m)<-[:MainAct]-(:Person{name:\"" + mainActor + "\"})";
                    }
                }
                //演员名称
                if (movieInfoDto.actors != null)
                {
                    foreach (var actor in movieInfoDto.actors)
                    {
                        statement += " ,(m)<-[:Act]-(:Person{name:\"" + actor + "\"})";
                    }
                }

                Boolean whereAppear = false;
                //电影名称
                if (movieInfoDto.movieName != null)
                {
                    statement += " where m.title=\"" + movieInfoDto.movieName + "\" ";
                    whereAppear = true;
                }

                //最低评分
                if (movieInfoDto.minScore != null)
                {
                    if (whereAppear)
                        statement += " and ";
                    else
                    {
                        statement += " where ";
                        whereAppear = true;
                    }
                    statement += " m.score >=" + movieInfoDto.minScore + " ";
                }

                //最高评分
                if (movieInfoDto.maxScore != null)
                {
                    if (whereAppear)
                        statement += " and ";
                    else
                    {
                        statement += " where ";
                        whereAppear = true;
                    }
                    statement += " m.score <= " + movieInfoDto.maxScore + " ";
                }


                //上映时间
                if(movieInfoDto.minYear != null)
                {
                    if (whereAppear)
                        statement += " and ";
                    else
                    {
                        statement += " where ";
                        whereAppear = true;
                    }
                    statement += " m.year*10000 + m.month*100 + m.day >= " +
                        (10000 * movieInfoDto.minYear + 100 * movieInfoDto.minMonth + movieInfoDto.minDay) + " ";
                }
                //上映时间
                if (movieInfoDto.maxYear != null)
                {
                    if (whereAppear)
                        statement += " and ";
                    else
                    {
                        statement += " where ";
                        whereAppear = true;
                    }
                    statement += " m.year*10000 + m.month*100 + m.day <= " +
                        (10000 * movieInfoDto.maxYear + 100 * movieInfoDto.maxMonth + movieInfoDto.maxDay) + " ";
                }

                if(movieInfoDto.positive != null)
                {
                    if (whereAppear)
                        statement += " and ";
                    else
                    {
                        statement += " where ";
                        whereAppear = true;
                    }
                    statement += " m.positive >= " +
                        Convert.ToString(movieInfoDto.positive * 1.0 / 100) + " ";

                }

                statement += " return m";
                cursor = await session.RunAsync(statement);
                movies = await cursor.ToListAsync();

            }
            finally
            {
                await session.CloseAsync();
            }
            return movies;
        }

    }
}

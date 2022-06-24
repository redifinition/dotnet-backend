using DataWarehouse.IRepositories;
using DataWarehouse.Models;

namespace DataWarehouse.Services.ServiceImpl
{
    public class TraceabilityServiceImpl : ITraceabilityService
    {
        private readonly ITraceabilityRepository _tracebilityRepository;


        public TraceabilityServiceImpl(ITraceabilityRepository traceabilityRepository)
        {
            _tracebilityRepository = traceabilityRepository;
        }

        public async Task<Dictionary<string, object>> GetCommentListByPage(int currentPage, int pageSize)
        {
            return await _tracebilityRepository.GetCommentListByPage(currentPage, pageSize);
        }

        public async Task<Dictionary<string, object>> GetConsolidationMovieList(int currentPage, int pageSize, bool conflict)
        {
            List<TConsolidationMovie> consolidationMovies;
            int totalCount;
            if (conflict)
            {
                consolidationMovies = await _tracebilityRepository.GetConsolidationMoviesByAsinCountGreaterThan(currentPage, pageSize, 1);
                totalCount = await _tracebilityRepository.GetConsolidationMoviesCountGreaterThanAsinCount(1);
            }
            else
            {
                consolidationMovies = await _tracebilityRepository.GetConsolidationMoviesByAsinCountEquals(currentPage, pageSize, 1);
                totalCount = await _tracebilityRepository.GetConsolidationMoviesCountEqualAsinCount(1);
            }
            int totalPage;
            if (totalCount / pageSize == 0)
                totalPage = totalCount / pageSize;
            else
                totalPage = totalCount / pageSize + 1;

            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("consolidationMovieList", consolidationMovies);
            return result;
        }


        public async Task<Dictionary<string, object>> GetMissingMovieAsins(int currentPage, int pageSize)
        {
            return await _tracebilityRepository.GetMissingAsinList(currentPage, pageSize);
        }

        public async Task<Dictionary<string, object>> GetMovieList(int currentPage, int pageSize)
        {
            return await _tracebilityRepository.GetMovieList(currentPage, pageSize);
        }

        public async Task<Dictionary<string, object>> GetMovieTVAsinList(int currentPage, int pageSize)
        {
            return await _tracebilityRepository.GetMovieTvAsinList(currentPage, pageSize);
        }

        public async Task<Dictionary<string, object>> GetMovieVersionByAsin(string asin)
        {
            var result = new Dictionary<string, object>();
            var tconsolidationMovie = await _tracebilityRepository.FindConsolidationMovieByAsin(asin);
            if (tconsolidationMovie != null)
            {
                var asinCount = tconsolidationMovie.AsinCount;
                var sonAsinList = await _tracebilityRepository.FindFatherAsinsByasin(asin);
                if(sonAsinList != null)
                {
                    var sonAsins = new List<string>();
                    foreach(var sonAsin in sonAsinList)
                    {
                        sonAsins.Add(sonAsin.Asin);
                    }
                    sonAsins.Add(asin);
                    var cleaningMovieList = new List<TCleaningMovie>();
                    foreach(var sonAsin in sonAsins)
                    {
                        var cleaningMovie = await _tracebilityRepository.FindCleaningMovieByAsin(sonAsin);
                        if(cleaningMovie != null)
                        {
                            cleaningMovieList.Add(cleaningMovie);
                        }
                    }
                    long commentCount = await _tracebilityRepository.GetCommentCountsByAsinIn(sonAsins);
                    result.Add("versionCount", asinCount);
                    result.Add("movieList", cleaningMovieList);
                    result.Add("commentCount", commentCount);
                }
            }
            return result;

        }

        public async Task<Dictionary<string, object>> GetTotalCount()
        {
            var result = new Dictionary<string, object>();
            result.Add("commentCount", await _tracebilityRepository.GetCommentCount());
            result.Add("movieTvAsinCount", await _tracebilityRepository.GetmovieTVCount());
            result.Add("missingAsinCount", await _tracebilityRepository.GetMissingMovieTVCount());
            result.Add("movieCount", await _tracebilityRepository.GetCleaningMovieCount());
            result.Add("conflictConsolidationMovieCount",await _tracebilityRepository.GetConsolidationMoviesCountGreaterThanAsinCount(1));
            result.Add("noConflictConsolidationMovieCount", await _tracebilityRepository.GetConsolidationMoviesCountEqualAsinCount(1));
            result.Add("tvAsinCount", await _tracebilityRepository.GetTVAsinCount());
            return result;
        }

        public async Task<Dictionary<string, object>> GetTVAsinList(int currentPage, int pageSize)
        {
            return await _tracebilityRepository.GetTVAsinList(currentPage, pageSize);
        }

        public async Task<Dictionary<string, object>> SearchForTitle(string title, int currentPage, int pageSize)
        {
            var consolidationMovies = await _tracebilityRepository.GetConsolidationMoviesByMovieTitleLike(currentPage,pageSize,title);
            var result = new Dictionary<string,object>();
            result.Add("totalCount", 0);
            result.Add("totalPage", 0);
            if(consolidationMovies != null)
            {
                int totalCount = consolidationMovies.Count;
                int totalPage;
                if (totalCount / pageSize == 0)
                    totalPage = totalCount / pageSize;
                else
                    totalPage = totalCount / pageSize + 1;
                result["totalCount"] = totalCount;
                result["totalPage"] = totalPage;
                result.Add("consolidationMovieList", consolidationMovies);
            }
            return result;

        }
    }
}

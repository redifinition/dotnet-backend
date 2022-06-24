using DataWarehouse.Models;

namespace DataWarehouse.IRepositories
{
    public interface ITraceabilityRepository
    {
        public Task<TConsolidationMovie?> FindConsolidationMovieByAsin(string asin);

        public Task<List<TFatherAsin>?> FindFatherAsinsByasin(string asin);

        public Task<TCleaningMovie?> FindCleaningMovieByAsin(string asin);

        public Task<long> GetCommentCountsByAsinIn(List<string> asins);

        public Task<Dictionary<string, object>> GetCommentListByPage(int currentPage, int pageSize);

        public Task<Dictionary<string, object>> GetMissingAsinList(int currentPage, int pageSize);

        public Task<Dictionary<string, object>> GetMovieTvAsinList(int currentPage, int pageSize);

        public Task<Dictionary<string, object>> GetMovieList(int currentPage, int pageSize);

        public Task<List<TConsolidationMovie>> GetConsolidationMoviesByAsinCountGreaterThan(int curtentPage, int pageSize, int asinCount);

        public Task<List<TConsolidationMovie>> GetConsolidationMoviesByAsinCountEquals(int curtentPage, int pageSize, int asinCount);

        public Task<int> GetConsolidationMoviesCountGreaterThanAsinCount(int AsinCount);

        public Task<int> GetConsolidationMoviesCountEqualAsinCount(int AsinCount);

        public Task<Dictionary<string,object>> GetTVAsinList(int currentPage, int pageSize);

        public Task<int> GetCommentCount();

        public Task<int> GetmovieTVCount();

        public Task<int> GetMissingMovieTVCount();

        public Task<int> GetCleaningMovieCount();

        public Task<int> GetTVAsinCount();

        public Task<List<TConsolidationMovie>?> GetConsolidationMoviesByMovieTitleLike(int currentPage, int pageSize, string title);

    }
}

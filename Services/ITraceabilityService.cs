namespace DataWarehouse.Services
{
    public interface ITraceabilityService
    {
        Task<Dictionary<string, object>> GetMovieVersionByAsin(string asin);

        Task<Dictionary<string,object>> GetCommentListByPage(int currentPage, int pageSize);

        Task<Dictionary<string, object>> GetMissingMovieAsins(int currentPage, int pageSize);

        Task<Dictionary<string, object>> GetMovieTVAsinList(int currentPage, int pageSize);

        Task<Dictionary<string, object>> GetMovieList(int currentPage, int pageSize);

        Task<Dictionary<string, object>> GetConsolidationMovieList(int currentPage, int pageSize, bool conflict);

        Task<Dictionary<string, object>> GetTVAsinList(int currentPage, int pageSize);

        Task<Dictionary<string, object>> GetTotalCount();

        Task<Dictionary<string, object>> SearchForTitle(string title, int currentPage, int pageSize);

    }
}

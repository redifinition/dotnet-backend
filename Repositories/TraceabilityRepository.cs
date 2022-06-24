using DataWarehouse.IRepositories;
using DataWarehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Repositories
{
    public class TraceabilityRepository : ITraceabilityRepository
    {
        private readonly data_warehouseContext _data_warehouse_context;
        public TraceabilityRepository(data_warehouseContext data_warehouse_context)
        {
            _data_warehouse_context = data_warehouse_context;
        }

        public async Task<TCleaningMovie?> FindCleaningMovieByAsin(string asin)
        {
            return await _data_warehouse_context.TCleaningMovies.FirstAsync(x => x.Asin == asin);
        }

        public async Task<TConsolidationMovie?> FindConsolidationMovieByAsin(string asin)
        {
            return await _data_warehouse_context.TConsolidationMovies.FindAsync(asin);
        }

        public async Task<List<TFatherAsin>?> FindFatherAsinsByasin(string asin)
        {
            return await _data_warehouse_context.TFatherAsins.Where(s => s.Asin == asin).ToListAsync();
        }

        public async Task<long> GetCommentCountsByAsinIn(List<string> asins)
        {
            return await _data_warehouse_context.TComments.CountAsync(s => asins.Contains(s.Asin));
        }

        public async Task<Dictionary<string, object>> GetCommentListByPage(int currentPage, int pageSize)
        {
            int totalCount = await _data_warehouse_context.TComments.CountAsync();
            int totalPage = 0;
            if(totalCount / pageSize == 0)
                totalPage = totalCount/ pageSize;
            else
                totalPage = totalCount / pageSize + 1;
            List<TComment> commentList = await _data_warehouse_context.TComments.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("commentList", commentList);
            return result;
        }

        public async Task<List<TConsolidationMovie>> GetConsolidationMoviesByAsinCountEquals(int currentPage, int pageSize, int asinCount)
        {
            return await _data_warehouse_context.TConsolidationMovies.Where(s => s.AsinCount == asinCount).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<TConsolidationMovie>> GetConsolidationMoviesByAsinCountGreaterThan(int currentPage, int pageSize, int asinCount)
        {
            return await _data_warehouse_context.TConsolidationMovies.Where(s=> s.AsinCount > asinCount).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetConsolidationMoviesCountGreaterThanAsinCount(int asinCount)
        {
            return await _data_warehouse_context.TConsolidationMovies.Where(s=> s.AsinCount > asinCount).CountAsync();
        }

        public async Task<int> GetConsolidationMoviesCountEqualAsinCount(int asinCount)
        {
            return await _data_warehouse_context.TConsolidationMovies.Where(s => s.AsinCount == asinCount).CountAsync();
        }
        public async Task<Dictionary<string, object>> GetMissingAsinList(int currentPage, int pageSize)
        {
            List<VMissingMovieTv> vMissingMovieTvs = await _data_warehouse_context.VMissingMovieTvs.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            int totalCount = await _data_warehouse_context.VMissingMovieTvs.CountAsync();
            int totalPage = 0;
            if (totalCount / pageSize == 0)
                totalPage = totalCount / pageSize;
            else
                totalPage = totalCount / pageSize + 1;
            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("missingMovieAsin", vMissingMovieTvs);
            return result;
        }

        public async Task<Dictionary<string, object>> GetMovieList(int currentPage, int pageSize)
        {
            List<TCleaningMovie> movies = await _data_warehouse_context.TCleaningMovies.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            int totalCount = await _data_warehouse_context.TCleaningMovies.CountAsync();
            int totalPage = 0;
            if (totalCount / pageSize == 0)
                totalPage = totalCount / pageSize;
            else
                totalPage = totalCount / pageSize + 1;
            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("movieList", movies);
            return result;
        }

        public async Task<Dictionary<string, object>> GetMovieTvAsinList(int currentPage, int pageSize)
        {
            List<TMovieWithoutRemovingTvAsin> vMissingMovieTvs = await _data_warehouse_context.TMovieWithoutRemovingTvAsins.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            int totalCount = await _data_warehouse_context.TMovieWithoutRemovingTvAsins.CountAsync();
            int totalPage = 0;
            if (totalCount / pageSize == 0)
                totalPage = totalCount / pageSize;
            else
                totalPage = totalCount / pageSize + 1;
            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("asinList", vMissingMovieTvs);
            return result;
        }

        public async Task<Dictionary<string, object>> GetTVAsinList(int currentPage, int pageSize)
        {
            List<VTvAsin> TVAsins = await _data_warehouse_context.VTvAsins.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            int totalCount = await _data_warehouse_context.VTvAsins.CountAsync();
            int totalPage = 0;
            if (totalCount / pageSize == 0)
                totalPage = totalCount / pageSize;
            else
                totalPage = totalCount / pageSize + 1;
            var result = new Dictionary<string, object>();
            result.Add("totalCount", totalCount);
            result.Add("totalPage", totalPage);
            result.Add("tvAsinList", TVAsins);
            return result;
        }

        public async Task<int> GetCommentCount()
        {
            return await _data_warehouse_context.TComments.CountAsync();
        }

        public async Task<int> GetmovieTVCount()
        {
           return await _data_warehouse_context.TMovieWithoutRemovingTvAsins.CountAsync();
        }

        public async Task<int> GetMissingMovieTVCount()
        {
            return await _data_warehouse_context.VMissingMovieTvs.CountAsync();
        }

        public async Task<int> GetCleaningMovieCount()
        {
            return await _data_warehouse_context.TCleaningMovies.CountAsync();
        }

        public async Task<int> GetTVAsinCount()
        {
            return await _data_warehouse_context.VTvAsins.CountAsync();
        }

        public async Task<List<TConsolidationMovie>?> GetConsolidationMoviesByMovieTitleLike(int currentPage, int pageSize, string title)
        {
            return await _data_warehouse_context.TConsolidationMovies.Where(s => s.MovieTitle.StartsWith(title)).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}

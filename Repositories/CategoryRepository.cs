using DataWarehouse.IRepositories;
using Amzaon_DataWarehouse_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataWarehouseContext _datawarhouseContext;

        public CategoryRepository(DataWarehouseContext datawarehoustContext)
        {
            this._datawarhouseContext = datawarehoustContext;
        }

        public async Task<List<ViewCategoryName>> GetCategoriesByName(string? categoryName)
        {
            return await _datawarhouseContext.ViewCategoryNames.Where(s => s.CategoryName.StartsWith(categoryName)).Skip(0).Take(50).ToListAsync();
        }
    }
}

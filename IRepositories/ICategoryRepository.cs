using Amzaon_DataWarehouse_BackEnd.Models;

namespace DataWarehouse.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<ViewCategoryName>> GetCategoriesByName(string? categoryName);
    }
}

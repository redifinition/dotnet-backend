using Amzaon_DataWarehouse_BackEnd.Models;

namespace DataWarehouse.IRepositories
{
    public interface IActorRepository
    {
        Task<List<ViewActorName>> GetActorsByName(string? actorName);


    }
}

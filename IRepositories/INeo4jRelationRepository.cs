using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;

namespace DataWarehouse.IRepositories
{
    public interface INeo4jRelationRepository
    {
        Task<List<IRecord>> GetMaxCooperationTimesOfActorAndDirector();

        Task<List<IRecord>> GetMaxCooperationTimesOfActors();

    }
}

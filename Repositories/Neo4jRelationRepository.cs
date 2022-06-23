using DataWarehouse.IRepositories;
using Neo4j.Driver;

namespace DataWarehouse.Repositories
{
    public class Neo4jRelationRepository : INeo4jRelationRepository
    {
        private readonly IDriver _driver;

        public Neo4jRelationRepository(IDriver driver)
        {
            _driver = driver;
        }

        public async Task<List<IRecord>> GetMaxCooperationTimesOfActorAndDirector()
        {
            var result = new List<IRecord>();
            IResultCursor cursor;
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                var statement = "Match (p:Person)-[r:MainAct|Act]->(m:Movie)<-[a:Direct]-(q:Person) "
                    + "where id(p)<>id(q) return p.name,q.name,count(m) order by count(m) desc limit 1";
                cursor = await session.RunAsync(statement);
                result = await cursor.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }

        public async Task<List<IRecord>> GetMaxCooperationTimesOfActors()
        {
            var result = new List<IRecord>();
            IResultCursor cursor;
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                var statement = "Match (p:Person)-[r:MainAct|Act]->(m:Movie)<-[a:MainAct|Act]-(q:Person) "
                    + "where id(p)<>id(q) return p.name,q.name,count(m) order by count(m) desc limit 1";
                cursor = await session.RunAsync(statement);
                result = await cursor.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
    }
}

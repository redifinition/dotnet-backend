namespace DataWarehouse.Services
{
    public interface IMysqlAssociationService
    {
        Task<List<string>?> GetDirectorNamesByMovieAsin(string movieAsin);
    }
}

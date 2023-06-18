namespace GrpcGreeter.Models
{
    public class WorkersListDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string WorkersCollectionName { get; set; } = null!;
    }
}

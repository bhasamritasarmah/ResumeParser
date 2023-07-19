using ResumeServices;

namespace DatabaseConnectionSettings
{
    /// <summary>
    /// The class 'DatabaseSettings' is using the interface 'IDatabaseSettings'
    /// to get the names of the database connection string, database name, and
    /// collection name from the appsettings.json file.
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}

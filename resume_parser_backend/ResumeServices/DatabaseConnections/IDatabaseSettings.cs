namespace ResumeServices
{
    /// <summary>
    /// The interface 'IDatabaseSettings' declares all the methods required
    /// to access the database and the collections therein. The implementations
    /// for the methods are done in the corresponding file 'DatabaseSettings'.
    /// </summary>
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}

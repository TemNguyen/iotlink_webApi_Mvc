namespace iotlink_webapi.DataModels
{
    public interface IMongoDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CollectionName { get; set; }
    }
}

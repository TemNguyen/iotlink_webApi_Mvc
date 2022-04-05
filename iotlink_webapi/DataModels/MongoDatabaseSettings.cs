namespace iotlink_webapi.DataModels
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string CollectionName { get; set; }
    }
}

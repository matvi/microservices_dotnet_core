namespace Action.Common.mongo
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }

        public bool Seed { get; set; }
    }
}
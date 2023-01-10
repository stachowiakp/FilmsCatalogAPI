namespace FilmsCatalog.Settings
{
    public class MongoCS
    {
          public string Host { get; set; }
          public int Port { get; set; }

        public string ConnectionString { get { return $"mongodb://{Host}:{Port}"; } }
    }
    

    public class MongoUsersDB
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString { get { return $"mongodb://{Host}:{Port}"; } }
    }
}

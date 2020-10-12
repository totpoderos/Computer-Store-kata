namespace ComputerStore.Database
{
    public static class DatabaseContext
    {
        private static Entities _database;

        public static Entities GetEntities()
        {
            _database ??= new Entities();
            return _database;
        }
    }
}
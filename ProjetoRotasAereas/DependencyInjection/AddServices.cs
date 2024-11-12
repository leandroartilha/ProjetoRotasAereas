using MongoDB.Driver;

namespace ProjetoRotasAereas.DependencyInjection
{
    public static class AddServices
    {
        [STAThread]
        static void Main()
        {
        }
        public static void AddServicesInterfaces(this IServiceCollection services)
        {
            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IContextUser, ContextUser>();
        }

        public static void AddMongoDb(this IServiceCollection services, string connectionString)
        {
            var client = new MongoClient(connectionString);
            var databaseName = "Hotel";
            var database = client.GetDatabase(databaseName);
            services.AddSingleton<IMongoDatabase>(database);
        }
    }
}

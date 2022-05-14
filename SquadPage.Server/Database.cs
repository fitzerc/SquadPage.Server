using Npgsql;
using SquadPage.Server.Controllers;

namespace SquadPage.Server
{
    public class Database
    {
        private readonly IConfiguration _config;

        public Database(IConfiguration config)
        {
            _config = config;
        }

        public SquadInfo GetSquadInfo()
        {
            SquadInfo squad;

            using (var conn = new NpgsqlConnection(buildConString()))
            {
                using (var db = new NPoco.Database(conn))
                {
                    db.Connection?.Open();
                    squad = db.Fetch<SquadInfo>().First();
                    db.Connection?.Close();
                }
            }

            return squad;
        }

        private string buildConString()
        {
            var builder = new NpgsqlConnectionStringBuilder();
            builder.Host = _config["Cockroach:Host"];
            builder.Port = int.Parse(_config["Cockroach:Port"]);
            builder.Database = _config["Cockroach:Database"];
            builder.SslMode = SslMode.VerifyFull;
            builder.Options = _config["Cockroach:Options"];
            builder.Username = _config["Cockroach:Username"];
            builder.Password = _config["Cockroach:Password"];

            return builder.ConnectionString;
        }
    }
}

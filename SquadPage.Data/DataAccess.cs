using Microsoft.Extensions.Configuration;
using Npgsql;
using SquadPage.Data.Models;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public SquadInfoResp GetSquadInfo()
        {
            using var conn = new NpgsqlConnection(buildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);

            db.Connection?.Open();
            var dto = db.Fetch<Squad>().First();

            if (dto == null)
            {
                throw new Exception("Unable to retrieve data");
            }
            var squad = new SquadInfoResp()
            {
                Id = dto.Id,
                Name = dto.Name,
                Record = new SquadRecord(0, 0, 0) //calculate at runtime?
            };

            db.Connection?.Close();

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
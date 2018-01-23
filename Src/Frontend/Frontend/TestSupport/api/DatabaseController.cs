using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Business.Migrations.MySqlFactories;
using Dapper;
using MySql.Data.MySqlClient;

namespace TestSupport.api
{
    public class DatabaseController : ApiController
    {
        private readonly MySqlConnection _mySqlConnection;

        public DatabaseController()
        {
            _mySqlConnection = new MySqlConnection(MySqlDatabaseSetup.CreateConnectionString());
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Reset()
        {
            const string query = "TRUNCATE TABLE images";

            await _mySqlConnection.ExecuteAsync(query);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Migrations.MySqlFactories;
using Dapper;
using MySql.Data.MySqlClient;

namespace Business.Repositories
{
    public class ImageRepository
    {
        private readonly MySqlConnection _mySqlConnection;

        public ImageRepository()
        {
            _mySqlConnection = new MySqlConnection(MySqlDatabaseSetup.CreateConnectionString());
        }

        public async Task<string> GetImageById(string imageId)
        {
            const string query = "SELECT ImageBlob FROM images WHERE Id = @id";

            var parameters = new Dictionary<string, object>
            {
                {"@id", imageId}
            };

            var result = await _mySqlConnection.QueryAsync<string>(query, parameters);
            return result.FirstOrDefault();
        }

        public async Task UploadImage(string imageId, string imageData)
        {
            const string query = "INSERT INTO images (Id, ImageBlob) VALUES (@imageId, @imageBlob)";

            var paramaters = new Dictionary<string, object>
            {
                {"@imageId", imageId},
                {"imageBlob", imageData}
            };

            var result = await _mySqlConnection.ExecuteAsync(query, paramaters);
            if(result == 0)
                throw new Exception("Unable to store image");
        }
    }
}

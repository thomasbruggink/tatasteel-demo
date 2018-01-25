using System;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;
using UITests.Services;
using UITests.TestSupport.Api;
using UITests.TestSupport.Models;

namespace UITests.Bindings.Given
{
    [Binding]
    public class GivenProducts
    {
        [Given(@"the following product information is available")]
        public void GivenTheFollowingProductInformationIsAvailable(Table table)
        {
            ApiServiceMock.ResponseMessages.Clear();
            var imageClient = new ImageApiClient();

            foreach (var tableRow in table.Rows)
            {
                //| ProductId | Name         | ImageId     | Count |
                var productId = tableRow["ProductId"];
                var name = tableRow["Name"];
                var imageId = tableRow["ImageId"];
                var count = int.Parse(tableRow["Count"]);

                ApiServiceMock.ResponseMessages.Add(new Product
                {
                    Id = productId,
                    Name = name,
                    ImageId = imageId,
                    Availability = count
                });

                var path = $"./Content/{imageId}";
                if(!File.Exists(path))
                    continue;
                var imageData = File.ReadAllBytes(path);
                var base64 = Convert.ToBase64String(imageData);
                var result = imageClient.UploadImage(imageId, base64);
                if(result.Status != HttpStatusCode.OK)
                    throw new InvalidOperationException($"Could not save image: {result.Status}");
            }
        }
    }
}

using UITests.TestSupport.Models;

namespace UITests.TestSupport.Api
{
    public class ImageApiClient
    {
        private readonly TestSupportApiHelper _testSupportApiHelper;

        public ImageApiClient()
        {
            _testSupportApiHelper = new TestSupportApiHelper();
        }

        public ApiResponse UploadImage(string imageId, string imageData)
        {
            var url = "/api/image/upload";

            var response = _testSupportApiHelper.Post(url, new ImageForm
            {
                ImageId = imageId,
                ImageData = imageData
            });

            return response;
        }
    }
}

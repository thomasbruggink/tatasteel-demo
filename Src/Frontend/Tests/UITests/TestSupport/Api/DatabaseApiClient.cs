namespace UITests.TestSupport.Api
{
    public class DatabaseApiClient
    {
        private readonly TestSupportApiHelper _testSupportApiHelper;

        public DatabaseApiClient()
        {
            _testSupportApiHelper = new TestSupportApiHelper();
        }

        public ApiResponse ResetDatabase()
        {
            var url = "/api/database/reset";

            var response = _testSupportApiHelper.Get(url);

            return response;
        }
    }
}
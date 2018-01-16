using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Api.IntegrationTests.Helpers
{
    public class ApiResultTable
    {
        private static ApiResultTable _instance;

        private readonly Dictionary<string, HttpResponseMessage> _nameResult;

        private ApiResultTable()
        {
            _nameResult = new Dictionary<string, HttpResponseMessage>();
        }

        public static ApiResultTable Instance => _instance ?? (_instance = new ApiResultTable());

        /// <summary>
        ///     Adds a result to the internal dictionary.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The dictionary value</param>
        public void AddResult(string name, HttpResponseMessage result)
        {
            _nameResult.Add(name, result);
        }

        /// <summary>
        ///     Retrieves a list of all the responsemessages.
        /// </summary>
        /// <returns>The list of all the HttpResponseMessages</returns>
        public List<HttpResponseMessage> GetAllResults()
        {
            return _nameResult.Select(x => x.Value).ToList();
        }

        /// <summary>
        ///     Retrieves the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <returns>The HttpResponseMessage that belongs to the specified key.</returns>
        public HttpResponseMessage GetResultByName(string name)
        {
            return _nameResult[name];
        }

        /// <summary>
        ///     Tries to retrieve the value that belongs to the specified key.
        ///     Returns null when it does not exist.
        /// </summary>
        /// <param name="name">The dictionary key.</param>
        /// <returns>Either the HttpResponseMessage that belongs to the specified key or null.</returns>
        public HttpResponseMessage TryGetResultByName(string name)
        {
            HttpResponseMessage message = null;
            _nameResult.TryGetValue(name, out message);
            return message;
        }

        /// <summary>
        ///     Gets the key value pairs.
        /// </summary>
        public IEnumerable<KeyValuePair<string, HttpResponseMessage>> GetKeyValuePairs()
        {
            return _nameResult.ToList();
        }

        /// <summary>
        ///     Updates the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The result to store</param>
        public void UpdateResultByName(string name, HttpResponseMessage result)
        {
            _nameResult[name] = result;
        }

        /// <summary>
        ///     Destroys the current instance.
        /// </summary>
        public static void Reset()
        {
            _instance = null;
        }
    }
}
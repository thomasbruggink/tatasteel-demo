using System.Collections.Generic;
using System.Linq;

namespace Backend.IntegrationTests.Helpers
{
    public class ProductResultTable
    {
        private static ProductResultTable _instance;

        private readonly Dictionary<string, int> _nameResult;

        private ProductResultTable()
        {
            _nameResult = new Dictionary<string, int>();
        }

        public static ProductResultTable Instance => _instance ?? (_instance = new ProductResultTable());

        /// <summary>
        ///     Adds a result to the internal dictionary.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The dictionary value</param>
        public void AddResult(string name, int result)
        {
            _nameResult.Add(name, result);
        }

        /// <summary>
        ///     Retrieves a list of all the responsemessages.
        /// </summary>
        /// <returns>The list of all the HttpResponseMessages</returns>
        public List<int> GetAllResults()
        {
            return _nameResult.Select(x => x.Value).ToList();
        }

        /// <summary>
        ///     Retrieves the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <returns>The HttpResponseMessage that belongs to the specified key.</returns>
        public int GetResultByName(string name)
        {
            return _nameResult[name];
        }

        /// <summary>
        ///     Tries to retrieve the value that belongs to the specified key.
        ///     Returns null when it does not exist.
        /// </summary>
        /// <param name="name">The dictionary key.</param>
        /// <returns>Either the HttpResponseMessage that belongs to the specified key or null.</returns>
        public int TryGetResultByName(string name)
        {
            _nameResult.TryGetValue(name, out var message);
            return message;
        }

        /// <summary>
        ///     Gets the key value pairs.
        /// </summary>
        public IEnumerable<KeyValuePair<string, int>> GetKeyValuePairs()
        {
            return _nameResult.ToList();
        }

        /// <summary>
        ///     Updates the value that belongs to the specified key.
        /// </summary>
        /// <param name="name">The dictionary key</param>
        /// <param name="result">The result to store</param>
        public void UpdateResultByName(string name, int result)
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

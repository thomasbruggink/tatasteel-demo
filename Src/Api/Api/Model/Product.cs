namespace Api.Model
{
    /// <summary>
    ///     Contains product information
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     The product Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The imageId to retrieve image data
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        ///     How many of this product are in stock
        ///     Null if this information could not be retrieved
        /// </summary>
        public int? Availability { get; set; }
    }
}
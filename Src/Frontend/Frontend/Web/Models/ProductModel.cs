namespace Web.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageId { get; set; }
        public string ImageType { get; set; }
        public string ImageBlob { get; set; }
        public int? Count { get; set; }
    }
}
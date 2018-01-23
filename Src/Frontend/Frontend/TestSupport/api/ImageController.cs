using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Business.Repositories;
using TestSupport.Models;

namespace TestSupport.api
{
    [AllowAnonymous]
    public class ImageController : ApiController
    {
        private readonly ImageRepository _imageRepository;

        public ImageController()
        {
            _imageRepository = new ImageRepository();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Upload([FromBody] ImageForm imageForm)
        {
            await _imageRepository.UploadImage(imageForm.ImageId, imageForm.ImageData);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
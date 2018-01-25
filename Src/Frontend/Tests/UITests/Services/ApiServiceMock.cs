using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using UITests.TestSupport.Models;

namespace UITests.Services
{
    public class ApiServiceMock
    {
        public static HttpStatusCode ResponseCode;
        public static List<Product> ResponseMessages;

        public void Configuration(IAppBuilder app)
        {
            app.Run(ApiServiceMiddleware);
        }

        public Task ApiServiceMiddleware(IOwinContext context)
        {
            if (ResponseCode != HttpStatusCode.OK)
            {
                context.Response.StatusCode = (int)ResponseCode;
                return context.Response.WriteAsync("");
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseMessages));
        }
    }
}

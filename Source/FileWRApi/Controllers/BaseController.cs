using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace FileWRApi.Controllers
{
    public class BaseController : ApiController
    {
        public HttpResponseException HandleException()
        {
            return new HttpResponseException(new HttpResponseMessage());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PortalProcessos.Api.PortalContext
{
    public class ApiException : HttpResponseException
    {
        public ApiException(string message)
            : base(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message), ReasonPhrase = message })
        {
        }

        public ApiException(HttpStatusCode code)
            : base(new HttpResponseMessage(code))
        {
        }

        public ApiException(string message, HttpStatusCode code)
            : base(new HttpResponseMessage(code) { Content = new StringContent(message), ReasonPhrase = message })
        {
        }
    }
}

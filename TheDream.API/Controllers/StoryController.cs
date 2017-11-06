using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TheDream.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Story")]
    public class StoryController : ApiController
    {
        [Route("GetStory")]
        public HttpResponseMessage GetStory()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "This is a story");
        }
    }
}

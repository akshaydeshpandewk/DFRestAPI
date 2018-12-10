using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DFRestAPI.Controllers
{
    [RoutePrefix("dashboardframework")]
    public class DFController : ApiController
    {
        [Route("createdb")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CreateDatabase()
        {
            CosmosDBHelper.CreateDB().Wait(5000);
            return  Ok(true); 
        }

        [Route("createcollection")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CreateCollection()
        {
            CosmosDBHelper.CreateCollection().Wait(5000);
            return Ok(true);
        }

        [Route("createdocument")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CreateDocument()
        {
            CosmosDBHelper.CreateDocument().Wait(5000);
            return Ok(true);
        }
    }
}

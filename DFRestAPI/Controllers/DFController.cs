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
        [Route("dashboardframeworkrestapi")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDashboardFrameworkLayout()
        {
            return  Ok(false); ;
        }
    }
}

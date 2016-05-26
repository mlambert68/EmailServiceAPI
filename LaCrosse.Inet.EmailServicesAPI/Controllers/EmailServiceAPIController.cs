using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LaCrosse.Inet.EmailServicesAPI.Models;

namespace LaCrosse.Inet.EmailServicesAPI.Controllers
{
    public class EmailServiceAPIController : ApiController
    {
        [Route("api/send")]
        [HttpPost]
        private string SendEmail(EmailMessageContent sentEmail)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Received email message from {0}: , To {1}, Subject {2}", sentEmail.From, sentEmail.To, sentEmail.Subject);
            sentEmail.Attachments.ForEach(i =>
                sb.AppendFormat("Got attachment {0} of type {1} and size {2} bytes,", i.FileName, i.MimeType,
                    i.FileData.Length)
                );

            var result = sb.ToString();
            Trace.Write(result);

            return result;

        }
    
        static List<string> data = initList();
        private static List<string> initList()
        {
            var ret = new List<string>();
            ret.Add("Hello from Email API at " + DateTime.Now.ToString());
            ret.Add("This is Mark" );

            return ret;
        }
        public IEnumerable<string> Get()
        {
            return data;
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            if (data.Count > id)
            {
                return Request.CreateResponse<string>(HttpStatusCode.OK, data[id]);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");

            }

        }
        [Route ("api/Test")]
        [HttpPost]
        public void Post([FromBody]string something)
        {
            data.Add(something);
        }
    }
}
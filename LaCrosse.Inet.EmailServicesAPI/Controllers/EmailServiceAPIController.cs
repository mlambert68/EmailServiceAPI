using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using LaCrosse.Inet.EmailServicesAPI.Models;

namespace LaCrosse.Inet.EmailServicesAPI.Controllers
{
    public class EmailServiceAPIController : ApiController
    {
       

        [Route("api/send")]
        [HttpPost]
        private HttpResponseMessage SendEmail(EmailMessageContent sentEmail)
        /*void sendEmail(string strFrom
                            , string strTo
                            , string strSubject
                            , string strBody
                            , string strAttachment)*/
        {

            /// Send an email using NG SMTP server

            char[] delimiterChars = { ',', ';' };
            string[] strToNames = sentEmail.To.Split(delimiterChars);

            MailMessage objMailMessage = new MailMessage();

            System.Net.NetworkCredential objSMTPUserInfo = new System.Net.NetworkCredential();
            SmtpClient objSmtpClient = new SmtpClient();

            try
            {
                objMailMessage.From = new MailAddress(sentEmail.From);

                //Add each To address to the message
                foreach (string s in strToNames)
                {
                    objMailMessage.To.Add(new MailAddress(s));
                }

                //Setup message
                objMailMessage.Subject = sentEmail.Subject;
                objMailMessage.Body = sentEmail.Body;

                //Get attachments
                foreach (var i in sentEmail.Attachments)
                {
                    Stream iBytedata = new MemoryStream(i.FileData);
                    objMailMessage.Attachments.Add(new Attachment(iBytedata, i.FileName, i.MimeType));
                }

                objSmtpClient = new SmtpClient("intmail.ngic.com"); /// NatGen Server IP
                objSMTPUserInfo = new System.Net.NetworkCredential
                ("AH-APP-PL-Dev", "&uThuC5A", "NGIC");
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //Send Message
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            { throw ex; }

            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }

            return Request.CreateResponse<string>(HttpStatusCode.OK, "Email Sent");
        }
        [Route("api/test")]
        [HttpPost]
        private String SendEmailTest([FromBody]widget sentEmail)
        {
            var sb = new StringBuilder();
            
            sb.AppendFormat("Received email message from {0}: , To {1}, Subject {2}", sentEmail.ID, sentEmail.Name, sentEmail.Price);
            //sentEmail.Attachments.ForEach(i =>
            //    sb.AppendFormat("Got attachment {0} of type {1} and size {2} bytes,", i.FileName, i.MimeType,
            //        i.FileData.Length)
            //    );

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
        [Route ("api/sendTest")]
        [HttpPost]
        public void Post([FromBody]string something)
        {
            data.Add(something);
        }
    }
}
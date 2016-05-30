using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using LaCrosse.Inet.EmailServicesAPI.Models;

namespace LaCrosse.Inet.EmailServicesAPI.Utils
{
    public class EmailMessageContentMediaTypeFormatter : MediaTypeFormatter
    {
        public EmailMessageContentMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(EmailMessageContent);
        }

        public override bool CanWriteType(Type type)
        {
            return false;                       
        }
        public async override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var provider = await content.ReadAsMultipartAsync();

            var modelContent = provider.Contents.FirstOrDefault(c => c.Headers.ContentDisposition.Name.NormalizeName() == "emailmessagecontent");

            var EmailFileSet = await modelContent.ReadAsAsync<EmailMessageContent>();

            var fileContents = provider.Contents
                .Where(c => c.Headers.ContentDisposition.Name.NormalizeName().Matches(@"file\d+"))
                .ToList();

            EmailFileSet.Attachments = new List<FileAttachment>();
            foreach (var fileContent in fileContents)
            {
                EmailFileSet.Attachments.Add(new FileAttachment
                {
                    FileData = await fileContent.ReadAsByteArrayAsync(),
                    MimeType = fileContent.Headers.ContentType.MediaType,
                    FileName = fileContent.Headers.ContentDisposition.FileName.NormalizeName()
                });
            }

            return EmailFileSet;

        }
    }

    public static class StringExtenstions
    {
        public static string NormalizeName(this string text)
        {
            return text.Replace("\"", "");
        }

        public static bool Matches(this string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }
    }
}

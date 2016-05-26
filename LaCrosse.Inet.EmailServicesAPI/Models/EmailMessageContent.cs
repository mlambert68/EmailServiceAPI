﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCrosse.Inet.EmailServicesAPI.Models
{
    public class EmailMessageContent
    {
        public string From { get; set; }
        public string To { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<FileAttachment> Attachments { get; set; }
    }

        public class FileAttachment
        {
            public string FileName { get; set; }

            public string MimeType { get; set; }

            public byte[] FileData { get; set; }
        }
    
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.API.Models
{
    public class MailRequest
    {
        public int InvoiceId { get; set; }
        public string MailTo { get; set; }
    }
}
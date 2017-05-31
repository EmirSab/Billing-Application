using System;

namespace Billing.Api.Models
{
    public class RequestModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // filter Id: Category, Product, Customer, Agent...
        public int Id { get; set; }             
    }
}
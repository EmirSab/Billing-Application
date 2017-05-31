using System;

namespace Billing.Api.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Remember { get; set; }
        public CurrentUserModel CurrentUser { get; set; }
    }
}
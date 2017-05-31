namespace Billing.Database
{
    public class ApiUser : Basic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string AppId { get; set; }
    }
}

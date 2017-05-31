using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class AgentModel
    {
        public AgentModel()
        {
            Towns = new List<TownModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public List<TownModel> Towns { get; set; }
    }
}
using Billing.Database;

namespace Billing.Repository
{
    public class InvoicesRepository : BillingRepository<Invoice>
    {
        public InvoicesRepository(BillingContext context) : base(context) { }
        public override void Update(Invoice entity, int id)
        {
            Invoice oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Agent= entity.Agent;
                oldEntity.Customer = entity.Customer;
                oldEntity.Shipper = entity.Shipper;
            }
        }
    }
}

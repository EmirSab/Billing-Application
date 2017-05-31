using Billing.Database;

namespace Billing.Repository
{
    public class ProcurementsRepository : BillingRepository<Procurement>
    {
        public ProcurementsRepository(BillingContext context) : base(context) { }

        public override void Update(Procurement entity, int id)
        {
            Procurement oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Product = entity.Product;
                oldEntity.Supplier = entity.Supplier;
            }
        }
    }
}
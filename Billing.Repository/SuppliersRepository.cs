using Billing.Database;

namespace Billing.Repository
{
    public class SuppliersRepository : BillingRepository<Supplier>
    {
        public SuppliersRepository(BillingContext context) : base(context) { }

        public override void Update(Supplier entity, int id)
        {
            Supplier oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Town = entity.Town;
            }
        }
    }
}
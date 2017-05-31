using Billing.Database;

namespace Billing.Repository
{
    public class ShippersRepository : BillingRepository<Shipper>
    {
        public ShippersRepository(BillingContext context) : base(context) { }

        public override void Update(Shipper entity, int id)
        {
            Shipper oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Town = entity.Town;
            }
        }
    }
}
using Billing.Database;

namespace Billing.Repository
{
    public class ItemsRepository: BillingRepository<Item>
    {
        public ItemsRepository(BillingContext context) : base(context) { }
        public override void Update(Item entity, int id)
        {
            Item oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Product = entity.Product;
                oldEntity.Invoice = entity.Invoice;
            }
        }
    }
}

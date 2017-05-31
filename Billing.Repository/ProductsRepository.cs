using Billing.Database;
using System.Linq;

namespace Billing.Repository
{
    public class ProductsRepository : BillingRepository<Product>
    {
        public ProductsRepository(BillingContext context) : base(context) { }

        public override void Update(Product entity, int id)
        {
            Product oldEntity = Get(id);
            if (oldEntity != null)
            {
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                oldEntity.Category = entity.Category;
            }
        }
    }
}

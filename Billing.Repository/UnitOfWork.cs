using Billing.Database;
using System;

namespace Billing.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly BillingContext _context = new BillingContext();

        private IBillingRepository<Event> _history;
        private IBillingRepository<ApiUser> _apiUsers;
        private IBillingRepository<AuthToken> _tokens;
        private IBillingRepository<Category> _categories;
        private CustomersRepository _customers;
        private InvoicesRepository _invoices;
        private ItemsRepository _items;
        private ProcurementsRepository _procurements;
        private ProductsRepository _products;
        private ShippersRepository _shippers;
        private IBillingRepository<Stock> _stocks;
        private SuppliersRepository _suppliers;
        private IBillingRepository<Town> _towns;
        private AgentsRepository _agents;



        public BillingContext Context { get { return _context; } }

        public IBillingRepository<ApiUser>   ApiUsers     { get { return _apiUsers     ?? (_apiUsers =     new BillingRepository<ApiUser>(_context)); } }
        public IBillingRepository<AuthToken> Tokens       { get { return _tokens       ?? (_tokens =       new BillingRepository<AuthToken>(_context)); } }
        public IBillingRepository<Category>  Categories   { get { return _categories   ?? (_categories =   new BillingRepository<Category>(_context)); } }
        public CustomersRepository           Customers    { get { return _customers    ?? (_customers =    new CustomersRepository(_context)); } }
        public InvoicesRepository            Invoices     { get { return _invoices     ?? (_invoices =     new InvoicesRepository(_context)); } }
        public ItemsRepository               Items        { get { return _items        ?? (_items =        new ItemsRepository(_context)); } }
        public ProcurementsRepository        Procurements { get { return _procurements ?? (_procurements = new ProcurementsRepository (_context)); } }
        public ProductsRepository            Products     { get { return _products     ?? (_products =     new ProductsRepository(_context)); } }
        public ShippersRepository            Shippers     { get { return _shippers     ?? (_shippers =     new ShippersRepository(_context)); } }
        public IBillingRepository<Stock>     Stocks       { get { return _stocks       ?? (_stocks =       new BillingRepository<Stock>(_context)); } }
        public SuppliersRepository           Suppliers    { get { return _suppliers    ?? (_suppliers =    new SuppliersRepository(_context)); } }
        public IBillingRepository<Town>      Towns        { get { return _towns        ?? (_towns =        new BillingRepository<Town>(_context)); } }
        public IBillingRepository<Event> History { get { return _history ?? (_history = new BillingRepository<Event>(_context)); } }
        public AgentsRepository             Agents           { get { return _agents       ?? (_agents  =       new AgentsRepository(_context)); } }


        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

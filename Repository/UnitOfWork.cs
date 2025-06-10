using Dreslay.Models;

namespace Dreslay.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DresslyContext _context;

        public IGenericRepository<Admin> Admins { get; }
        public IGenericRepository<Client> Clients { get; }
        public IGenericRepository<Tailor> Tailors { get; }
        public IGenericRepository<Fabric> Fabrics { get; }
        public IGenericRepository<Feedback> Feedbacks { get; }
        public IGenericRepository<Category> Categorys { get; }
        public IGenericRepository<Favorite> Favorites { get; }
        public IGenericRepository<Complaint> Complaints { get; }
        public IOfferRepository Offers { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public UnitOfWork(DresslyContext context)
        {
            _context = context;
            Admins = new GenericRepository<Admin>(_context);
            Clients = new GenericRepository<Client>(_context);
            Tailors = new GenericRepository<Tailor>(_context);
            Fabrics = new GenericRepository<Fabric>(_context);
            Feedbacks = new GenericRepository<Feedback>(_context);
            Categorys = new GenericRepository<Category>(_context);
            Favorites = new GenericRepository<Favorite>(_context);
            Complaints = new GenericRepository<Complaint>(_context);
            Offers = new OfferRepository(_context);


        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}

using Dreslay.Models;
namespace Dreslay.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Admin> Admins { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<Tailor> Tailors { get; }
        IGenericRepository<Fabric> Fabrics { get; }
        IGenericRepository<Feedback> Feedbacks { get; }
        IGenericRepository<Category> Categorys { get; }
        IGenericRepository<Favorite> Favorites { get; }
        IGenericRepository<Complaint> Complaints { get; }
        IOfferRepository Offers { get; }
        Task<int> CompleteAsync();

        Task<int> SaveAsync();
    }
}


using Dreslay.Models;
using Dreslay.Repository;

public interface IOfferRepository : IGenericRepository<Offer>
{
    Task<IEnumerable<Offer>> GetByTailorIdAsync(int tailorId);
}

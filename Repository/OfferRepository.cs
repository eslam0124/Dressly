using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.EntityFrameworkCore;

public class OfferRepository : GenericRepository<Offer>, IOfferRepository
{
    private readonly DresslyContext _context;

    public OfferRepository(DresslyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Offer>> GetByTailorIdAsync(int tailorId)
    {
        return await _context.Offers
            .Where(o => o.TailorId == tailorId)
            .ToListAsync();
    }
}

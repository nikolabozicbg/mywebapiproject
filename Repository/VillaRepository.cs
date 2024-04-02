using MyWebApiProject.Data;
using MyWebApiProject.Models;
using MyWebApiProject.Repository.IRepository;

namespace MyWebApiProject.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly AppliationDbContext _db;


    public VillaRepository(AppliationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Villa> UpdateAsync(Villa villa)
    {
        villa.UpdatedAt = DateTime.Now;
        _db.Villas.Update(villa);
        await _db.SaveChangesAsync();
        return villa;
    }
}
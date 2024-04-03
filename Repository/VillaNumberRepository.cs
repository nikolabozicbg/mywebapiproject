using MyWebApiProject.Data;
using MyWebApiProject.Models;
using MyWebApiProject.Repository.IRepository;

namespace MyWebApiProject.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly AppliationDbContext _db;


    public VillaNumberRepository(AppliationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _db.VillaNumbers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
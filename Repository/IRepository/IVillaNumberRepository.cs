using MyWebApiProject.Models;

namespace MyWebApiProject.Repository.IRepository;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
    Task<VillaNumber> UpdateAsync(VillaNumber villa);
}
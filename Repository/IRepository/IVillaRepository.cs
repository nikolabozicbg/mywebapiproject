using MyWebApiProject.Models;

namespace MyWebApiProject.Repository.IRepository;

public interface IVillaRepository : IRepository<Villa>
{
    Task<Villa> UpdateAsync(Villa villa);
}
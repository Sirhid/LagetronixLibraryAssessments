using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data
{
    public interface IGenericRepo<T>
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task UpdateAsync(T entity);
    }
}
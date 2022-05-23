using LagetonixLibraryAssessment.Business.GenericResponse;
using LagetonixLibraryAssessment.Data.DTO_s;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.Repository
{
    public interface ICategoryRepository
    {
        Task<BaseResponse> CreateCategory(CreateCategoryDTO payload);
        Task<BaseResponse> GetAllCategories();
        Task<BaseResponse> GetAllCategoryByID(long CategoryId);
        Task<BaseResponse> UpdateCategory(UpdateCategoryDTO payload);
        Task<BaseResponse> DeleteCategory(DeleteCategoryDTO payload);
    }
}
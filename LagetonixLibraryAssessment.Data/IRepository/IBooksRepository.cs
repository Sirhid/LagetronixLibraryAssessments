using LagetonixLibraryAssessment.Business.GenericResponse;
using LagetonixLibraryAssessment.Data.DTO_s;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.Repository
{
    public interface IBooksRepository
    {
        Task<BaseResponse> GetAllBooks();
        Task<BaseResponse> GetAllBooksByID(long bookid);
        Task<BaseResponse> GetAllFavoriteBooks();
        Task<BaseResponse> CreateBook(CreateBooksDTO payload);
        Task<BaseResponse> UpdateBook(UpdateBooksDTO payload);
        Task<BaseResponse> DeleteBook(DeleteBooksDTO payload);
    }
}
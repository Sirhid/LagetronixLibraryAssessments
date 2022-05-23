using LagetonixLibraryAssessment.Business.GenericResponse;
using LagetonixLibraryAssessment.Data.AppContants;
using LagetonixLibraryAssessment.Data.DTO_s;
using LagetonixLibraryAssessment.Data.Repository;
using LagetonixLibraryAssessment.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.IRepository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ILogger<BooksRepository> _logger;
        private readonly AppDbContext _appDbContext;
        public BooksRepository(ILogger<BooksRepository> logger, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<BaseResponse> GetAllBooks()
        {
            try
            {
                var Response = new BaseResponse();
                var booklist = await _appDbContext.Books.Where(x=>!x.isDeleted).ToListAsync();
                if (booklist.Count>0)
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.SuccessResponseCode.ToString();
                    Response.ResponseMessage = "Successfully retrieved";
                    return Response;
                }
                else
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.NotFoundStatusCode.ToString();
                    Response.ResponseMessage = "No Record Found";
                    return Response;
                }
                
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: GetAllBooks() ===>{ex.Message}");
                throw;
            }
        }

        public async Task<BaseResponse> GetAllFavoriteBooks()
        {
            try
            {
                var Response = new BaseResponse();
                var booklist = await _appDbContext.Books.Where(x=>x.IsFavorite).ToListAsync();
                if (booklist.Count > 0)
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.SuccessResponseCode.ToString();
                    Response.ResponseMessage = "Successfully retrieved";
                    return Response;
                }
                else
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.NotFoundStatusCode.ToString();
                    Response.ResponseMessage = "No Record Found";
                    return Response;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: GetAllBooks() ===>{ex.Message}");
                throw;
            }
        }

        public async Task<BaseResponse> GetAllBooksByID(long bookid)
        {
            try
            {
                var Response = new BaseResponse();
                var booklist = await _appDbContext.Books.Where(x=>x.BookID==bookid && !x.isDeleted).SingleOrDefaultAsync();
                if (booklist!=null)
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.SuccessResponseCode.ToString();
                    Response.ResponseMessage = "Successfully retrieved";
                    return Response;
                }
                else
                {
                    Response.Data = booklist;
                    Response.ResponseCode = ApplicationConstants.NotFoundStatusCode.ToString();
                    Response.ResponseMessage = "No Record Found";
                    return Response;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: GetAllBooksByID() ===>{ex.Message}");
                throw;
            }
        }



        public async Task<BaseResponse> CreateBook(CreateBooksDTO payload)
        {
            try
            {
                var newpayload = new Books();
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                var allbooks = await _appDbContext.Books.Where(x => x.isDeleted == false).ToListAsync();
                var category = await _appDbContext.Catergories.Where(x => x.CategoryId == payload.CategoryId).SingleOrDefaultAsync();
                if (payload.BookName == null)
                {
                    
                    isModelStateValidate = false;
                    validationMessage = " Book Name cannot be empty";

                }
                if (category == null)
                {

                    isModelStateValidate = false;
                    validationMessage = "Category selected does not exist";

                }
                if (payload.CategoryId < 0)
                {
                   
                    isModelStateValidate = false;
                    validationMessage = "Book Category cannot be Empty";
                }

                if (!isModelStateValidate)
                {
                    return new BaseResponse()
                    { 
                        ResponseMessage = validationMessage,
                        ResponseCode = ApplicationConstants.FailureResponse.ToString(),
                        Data = null
                    };
                }
                if (allbooks.Any(x=>x.BookName==payload.BookName))
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Book Already Exist";
                    Response.Data = null;

                }
                newpayload.BookName = payload.BookName;
                newpayload.CategoryId = payload.CategoryId;
                newpayload.CreatedByUserID = payload.CreatedByUserID;
                newpayload.IsFavorite = payload.IsFavorite;
                newpayload.DateCreated = DateTime.Now;
                var addbook = await _appDbContext.AddAsync(newpayload);

                var savebook= await _appDbContext.SaveChangesAsync();

                if (savebook > 0)
                {
                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Book Saved Successfully";
                    Response.Data = payload;
                    return Response;


                }
                else {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Internal Server Error";
                    Response.Data = payload;
                    return Response;

                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: CreateBook() ===>{ex.Message}");
                throw;
            }

        }


        public async Task<BaseResponse> DeleteBook(DeleteBooksDTO payload)
        {
            try
            {
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                var newpayload = await _appDbContext.Books.Where(x => x.BookID == payload.BookID).SingleOrDefaultAsync();

                if (payload.BookID == 0)
                {

                    isModelStateValidate = false;
                    validationMessage = " Book ID cannot be empty";

                }
                if (payload! ==null)
                {

                    isModelStateValidate = false;
                    validationMessage = "No book found";

                }


                if (!isModelStateValidate)
                {
                    return new BaseResponse()
                    {
                        ResponseMessage = validationMessage,
                        ResponseCode = ApplicationConstants.FailureResponse.ToString(),
                        Data = null
                    };
                }
                
                newpayload.isDeleted = true;
                newpayload.BookID = payload.BookID;
                newpayload.DateDeleted = DateTime.Now;

                _appDbContext.Entry(newpayload).State = EntityState.Modified;
                int savebook = await _appDbContext.SaveChangesAsync();

                if (savebook > 0)
                {
                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Book Deleted Successfully";
                    Response.Data = payload;
                    return Response;


                }
                else
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Internal Server Error";
                    Response.Data = payload;
                    return Response;

                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: DeleteBook() ===>{ex.Message}");
                throw;
            }

        }


        public async Task<BaseResponse> UpdateBook(UpdateBooksDTO payload)
        {
            try
            {
                //var newpayload = new Books();
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                //var allbooks = await _appDbContext.Books.Where(x => x.isDeleted == false).ToListAsync();
                var newpayload = await _appDbContext.Books.Where(x => x.BookID == payload.BookID).SingleOrDefaultAsync();


                if (payload.BookName == null)
                {

                    isModelStateValidate = false;
                    validationMessage = " Book Name cannot be empty";

                }
                if (payload.CategoryId < 0)
                {

                    isModelStateValidate = false;
                    validationMessage = "Book Category cannot be Empty";
                }

                if (!isModelStateValidate)
                {
                    return new BaseResponse()
                    {
                        ResponseMessage = validationMessage,
                        ResponseCode = ApplicationConstants.FailureResponse.ToString(),
                        Data = null
                    };
                }
                if (newpayload.BookName == payload.BookName)
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Book Already Exist";
                    Response.Data = null;

                }
                newpayload.BookName = payload.BookName;
                newpayload.CategoryId = payload.CategoryId;
                newpayload.LastDateUpdated = DateTime.Now;
                newpayload.IsFavorite = payload.IsFavorite;
                newpayload.BookID = payload.BookID;
                newpayload.LastUpdatedByUserID = payload.LastUpdatedByUserID;


                var addbook = await _appDbContext.AddAsync(newpayload);
                _appDbContext.Entry(newpayload).State = EntityState.Modified;
                int savebook = await _appDbContext.SaveChangesAsync();
                //var savebook = await  _appDbContext.Books.Update(singlebook).CurrentValues.SetValues(newpayload); 

                if (savebook>0)
                {

                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Book Updated Successfully";
                    Response.Data = payload;
                    return Response;


                }
                else
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Internal Server Error";
                    Response.Data = payload;
                    return Response;

                }


            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: UpdatedBook() ===>{ex.Message}");
                throw;
            }

        }


    }
}

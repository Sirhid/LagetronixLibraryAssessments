using LagetonixLibraryAssessment.Business.GenericResponse;
using LagetonixLibraryAssessment.Data.AppContants;
using LagetonixLibraryAssessment.Data.DTO_s;
using LagetonixLibraryAssessment.Data.Repository;
using LagetonixLibraryAssessment.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.IRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly AppDbContext _appDbContext;
        public CategoryRepository(ILogger<CategoryRepository> logger, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<BaseResponse> GetAllCategories()
        {
            try
            {
                var Response = new BaseResponse();
                var Catergories = await _appDbContext.Catergories.ToListAsync();
                if (Catergories.Count > 0)
                {
                    Response.Data = Catergories;
                    Response.ResponseCode = ApplicationConstants.SuccessResponseCode.ToString();
                    Response.ResponseMessage = "Successfully retrieved";
                    return Response;
                }
                else
                {
                    Response.Data = Catergories;
                    Response.ResponseCode = ApplicationConstants.NotFoundStatusCode.ToString();
                    Response.ResponseMessage = "No Record Found";
                    return Response;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: GetAllCategories() ===>{ex.Message}");
                throw;
            }
        }

        public async Task<BaseResponse> GetAllCategoryByID(long CategoryId)
        {
            try
            {
                var Response = new BaseResponse();
                var Catergories = await _appDbContext.Catergories.Where(x => x.CategoryId == CategoryId).SingleOrDefaultAsync();
                if (Catergories != null)
                {
                    Response.Data = Catergories;
                    Response.ResponseCode = ApplicationConstants.SuccessResponseCode.ToString();
                    Response.ResponseMessage = "Successfully retrieved";
                    return Response;
                }
                else
                {
                    Response.Data = Catergories;
                    Response.ResponseCode = ApplicationConstants.NotFoundStatusCode.ToString();
                    Response.ResponseMessage = "No Record Found";
                    return Response;
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                _logger.LogError($"MethodName: GetAllCategoryByID() ===>{ex.Message}");
                throw;
            }
        }

        public async Task<BaseResponse> CreateCategory(CreateCategoryDTO payload)
        {
            try
            {
                var newpayload = new Catergory();
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                var Catergories = await _appDbContext.Catergories.Where(x => x.isDeleted == false).ToListAsync();

                if (payload.CategoryName == null)
                {

                    isModelStateValidate = false;
                    validationMessage = " Category Name cannot be empty";

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
                if (Catergories.Any(x => x.CategoryName == payload.CategoryName))
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Category Already Exist";
                    Response.Data = null;

                }
                newpayload.CategoryName = payload.CategoryName;
                newpayload.CreatedByUserID = payload.CreatedByUserID;
                newpayload.DateCreated = DateTime.Now;
                var addCategory = await _appDbContext.AddAsync(newpayload);

                var saveCategory = await _appDbContext.SaveChangesAsync();

                if (saveCategory > 0)
                {
                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Category Saved Successfully";
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
                _logger.LogError($"MethodName: CreateCategory() ===>{ex.Message}");
                throw;
            }

        }
        public async Task<BaseResponse> DeleteCategory(DeleteCategoryDTO payload)
        {
            try
            {
                //var newpayload = new Catergory();
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                var newpayload = await _appDbContext.Catergories.Where(x => x.CategoryId == payload.CategoryId).SingleOrDefaultAsync();

                if (payload.CategoryId == 0)
                {

                    isModelStateValidate = false;
                    validationMessage = " Book ID cannot be empty";

                }
                if (payload! == null)
                {

                    isModelStateValidate = false;
                    validationMessage = "No Category found";

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
                newpayload.CategoryId = payload.CategoryId;
                newpayload.DateDeleted = DateTime.Now;
                newpayload.DeletedByUserID = payload.DeletedByUserID;

                _appDbContext.Entry(newpayload).State = EntityState.Modified;
                int deletebook = await _appDbContext.SaveChangesAsync();

                if (deletebook > 0)
                {
                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Category Deleted Successfully";
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
                _logger.LogError($"MethodName: DeleteCategory() ===>{ex.Message}");
                throw;
            }

        }
        public async Task<BaseResponse> UpdateCategory(UpdateCategoryDTO payload)
        {
            try
            {
                //var newpayload = new Books();
                string validationMessage = "";
                bool isModelStateValidate = true;

                var Response = new BaseResponse();
                var newpayload = await _appDbContext.Catergories.Where(x => x.CategoryId == payload.CategoryId).SingleOrDefaultAsync();


                if (payload.CategoryName == null)
                {

                    isModelStateValidate = false;
                    validationMessage = " Book Name cannot be empty";

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
                if (newpayload.CategoryName == payload.CategoryName)
                {
                    Response.ResponseCode = ApplicationConstants.FailureResponse.ToString();
                    Response.ResponseMessage = "Category Already Exist";
                    Response.Data = null;

                }
                newpayload.CategoryName = payload.CategoryName;
                newpayload.CategoryId = payload.CategoryId;
                newpayload.LastDateUpdated = DateTime.Now;
                newpayload.LastUpdatedByUserID = payload.LastUpdatedByUserID;


                var updatecat = await _appDbContext.AddAsync(newpayload);
                _appDbContext.Entry(newpayload).State = EntityState.Modified;
                int updatecategory = await _appDbContext.SaveChangesAsync();

                if (updatecategory > 0)
                {

                    Response.ResponseCode = ApplicationConstants.successResponseCode.ToString();
                    Response.ResponseMessage = "Category Updated Successfully";
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
                _logger.LogError($"MethodName: UpdatedCategory() ===>{ex.Message}");
                throw;
            }

        }

    }
}

using LagetonixLibraryAssessment.Business.GenericResponse;
using LagetonixLibraryAssessment.Data.DTO_s;
using LagetonixLibraryAssessment.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LagetronixLibraryAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;

        public CategoryController(IMemoryCache cache,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _cache = cache;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var cacheKey = "category";
            if (!_cache.TryGetValue(cacheKey, out BaseResponse category))
            {
                category = await _categoryRepository.GetAllCategories();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _cache.Set(cacheKey, category, cacheExpiryOptions);
            }
            return Ok(category);
        }

        [HttpGet("GetAllCategoryByID")]
        public async Task<IActionResult> GetAllCategoryByID(long categoryid)
        {
            var category = await _categoryRepository.GetAllCategoryByID(categoryid);
            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO payload)
        {
            var cacheKey = "category";
            var category = await _categoryRepository.CreateCategory(payload);
            _cache.Remove(cacheKey);

            return Ok(category);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO payload)
        {
            var cacheKey = "category";
            var category = await _categoryRepository.UpdateCategory(payload);
            _cache.Remove(cacheKey);
            return Ok(category);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryDTO payload)
        {
            var cacheKey = "category";
            var category = await _categoryRepository.DeleteCategory(payload);
            _cache.Remove(cacheKey);
            return Ok(category);
        }
    }
}

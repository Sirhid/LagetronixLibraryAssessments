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
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMemoryCache _cache;

        public BooksController(IBooksRepository booksRepository, ICategoryRepository categoryRepository, IMemoryCache memoryCache)
        {
            _booksRepository = booksRepository;
            _categoryRepository = categoryRepository;
            _cache = memoryCache;

        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var cacheKey = "book";
            if (!_cache.TryGetValue(cacheKey, out BaseResponse book))
            {
                 book = await _booksRepository.GetAllBooks();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _cache.Set(cacheKey, book, cacheExpiryOptions);
            }
            return Ok(book);
        }
        [HttpGet("GetAllfavoriteBooks")]
        public async Task<IActionResult> GetAllfavoriteBooks()
        {
            var cacheKey = "book";
            if (!_cache.TryGetValue(cacheKey, out BaseResponse book))
            {
                book = await _booksRepository.GetAllFavoriteBooks();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _cache.Set(cacheKey, book, cacheExpiryOptions);
            }
            return Ok(book);
        }

        [HttpGet("GetAllBooksByID")]
        public async Task<IActionResult> GetAllBooksByID(long bookid)
        {
            var book = await _booksRepository.GetAllBooksByID(bookid);
            return Ok(book);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(CreateBooksDTO payload)
        {
            var cacheKey = "book";
            var book = await _booksRepository.CreateBook(payload);
            _cache.Remove(cacheKey);
            return Ok(book);

        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBooksDTO payload)
        {
            var cacheKey = "book";
            var book = await _booksRepository.UpdateBook(payload);
            _cache.Remove(cacheKey);
            return Ok(book);
        }


        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(DeleteBooksDTO payload)
        {
            var cacheKey = "book";
            var book = await _booksRepository.DeleteBook(payload);
            _cache.Remove(cacheKey);
            return Ok(book);
        }
    }
}

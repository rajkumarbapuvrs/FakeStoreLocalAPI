using FakeStoreLocalAPI.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> Get()
        {
            using var dbContext = new FakeStoreDBContext();
            var items = await dbContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .ToListAsync();
            return items;
        }

        [Route("{productId}")]
        [HttpGet]
        [Authorize]
        public async Task<Models.Product> GetById(int productId)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .FirstOrDefaultAsync(p => p.Id == productId);
            return item;
        }

        [Route("limit/{limit}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> GetByLimit(int limit)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .OrderBy(p =>p.Id)
                .Take(limit).ToListAsync();
            return item;
        }
        [Route("sort/{sort}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> GetBySorting(string sort)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .ToListAsync();
            List<Models.Product> sortedItems = new List<Models.Product>();
            if (sort.ToLower() == "desc")
                sortedItems = item.OrderByDescending(i => i.Id).ToList();
            else
                sortedItems = item.OrderBy(i => i.Id).ToList();
            return sortedItems;
        }
        [Route("categories")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<string>> GetCategories()
        {
            using var dbContext = new FakeStoreDBContext();
            var items = await dbContext.CategoryDetail.Select(p => p.Name).ToListAsync();
           
            return items;
        }
        [Route("category/{category}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> GetByCategory(string category)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .ToListAsync();
            List<Models.Product> filteredItems = new List<Models.Product>();

            filteredItems = item.Where(i => i.CategoryDetail.Name.ToLower() == category.ToLower()).ToList();
            return filteredItems;
        }
    }
}

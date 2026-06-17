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
        FakeStoreDBContext fakeStoreDBContext;
        public Products(FakeStoreDBContext fakeStoreDBContext)
        {
            this.fakeStoreDBContext = fakeStoreDBContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> Get()
        {
            var items = await fakeStoreDBContext.Product
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
            var item = await fakeStoreDBContext.Product
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
            var item = await fakeStoreDBContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail)
                .OrderBy(p => p.Id)
                .Take(limit).ToListAsync();
            return item;
        }
        [Route("sort/{sort}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> GetBySorting(string sort)
        {
            var item = fakeStoreDBContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail);
            List<Models.Product> sortedItems = new List<Models.Product>();
            if (sort.ToLower() == "desc")
                sortedItems = await item.OrderByDescending(i => i.Id).ToListAsync();
            else
                sortedItems = await item.OrderBy(i => i.Id).ToListAsync();
            return sortedItems;
        }
        [Route("categories")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<string>> GetCategories()
        {
            var items = await fakeStoreDBContext.CategoryDetail.Select(p => p.Name).ToListAsync();
            return items;
        }
        [Route("category/{category}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Product>> GetByCategory(string category)
        {
            var item = fakeStoreDBContext.Product
                .Include(p => p.Rating)
                .Include(p => p.CategoryDetail);
            List<Models.Product> filteredItems = new List<Models.Product>();

            filteredItems = await item.Where(i => EF.Functions.Like(i.CategoryDetail.Name, category, "~*")).ToListAsync();
            return filteredItems;
        }
    }
}

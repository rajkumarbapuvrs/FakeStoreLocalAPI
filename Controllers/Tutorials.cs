using FakeStoreLocalAPI.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Tutorials : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Models.Tutorial>> GetAll()
        {
            using var dbContext = new FakeStoreDBContext();
            var items = await dbContext.Tutorial.ToListAsync();
            return items;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Models.Tutorial> GetById(int id)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Tutorial
                .FirstOrDefaultAsync(p => p.Id == id);
            return item ?? new Models.Tutorial();
        }

        [Route("title/{title}")]
        [HttpGet]
        public async Task<IEnumerable<Models.Tutorial>> GetByLimit(string title)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Tutorial.Where(i=>i.Title.Contains(title)).ToListAsync();
            return item;
        }

        [HttpPost]
        public async Task<int> Save(Models.Tutorial tutorial)
        {
            using var dbContext = new FakeStoreDBContext();
            await dbContext.Tutorial.AddAsync(tutorial);
            int result = await dbContext.SaveChangesAsync();
            return result;
        }

        [HttpPut]
        public async Task<int> Update(Models.Tutorial tutorial)
        {
            using var dbContext = new FakeStoreDBContext();
            dbContext.Entry(tutorial).State = EntityState.Modified;
            int result = await dbContext.SaveChangesAsync();
            return result;
        }

        [HttpDelete]
        public async Task<int> DeleteAll()
        {
            using var dbContext = new FakeStoreDBContext();
            var result = await dbContext.Tutorial.ExecuteDeleteAsync();
            return result;
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<string> DeleteById(int id)
        {
            using var dbContext = new FakeStoreDBContext();
            var item = await dbContext.Tutorial.FindAsync(id);

            if (item == null)
            {
                return "Not Found"; // Returns 404 if the item doesn't exist
            }

            // 2. Mark the item as deleted in the Change Tracker
            dbContext.Tutorial.Remove(item);

            // 3. Persist the change to the database
            await dbContext.SaveChangesAsync();

            return "No Content";
        }
    }
}

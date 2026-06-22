using FakeStoreLocalAPI.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tutorials : ControllerBase
    {
        FakeStoreDBContext fakeStoreDBContext;
        public Tutorials(FakeStoreDBContext fakeStoreDBContext)
        {
            this.fakeStoreDBContext = fakeStoreDBContext;
        }
        [HttpGet]
        public async Task<IEnumerable<Models.Tutorial>> GetAll()
        {
            var items = await fakeStoreDBContext.Tutorial.ToListAsync();
            return items;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Models.Tutorial> GetById(int id)
        {
            var item = await fakeStoreDBContext.Tutorial
                .FirstOrDefaultAsync(p => p.Id == id);
            return item ?? new Models.Tutorial();
        }

        [Route("title/{title}")]
        [HttpGet]
        public async Task<IEnumerable<Models.Tutorial>> GetByLimit(string title)
        {
            var item = await fakeStoreDBContext.Tutorial.Where(i=>i.Title.Contains(title)).ToListAsync();
            return item;
        }

        [HttpPost]
        public async Task<int> Save(Models.Tutorial tutorial)
        {
            await fakeStoreDBContext.Tutorial.AddAsync(tutorial);
            int result = await fakeStoreDBContext.SaveChangesAsync();
            return result;
        }

        [HttpPut]
        public async Task<int> Update(Models.Tutorial tutorial)
        {
            fakeStoreDBContext.Entry(tutorial).State = EntityState.Modified;
            int result = await fakeStoreDBContext.SaveChangesAsync();
            return result;
        }

        [HttpDelete]
        public async Task<int> DeleteAll()
        {
            var result = await fakeStoreDBContext.Tutorial.ExecuteDeleteAsync();
            return result;
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<string> DeleteById(int id)
        {
            var item = await fakeStoreDBContext.Tutorial.FindAsync(id);

            if (item == null)
            {
                return "Not Found"; // Returns 404 if the item doesn't exist
            }

            // 2. Mark the item as deleted in the Change Tracker
            fakeStoreDBContext.Tutorial.Remove(item);

            // 3. Persist the change to the database
            await fakeStoreDBContext.SaveChangesAsync();

            return "No Content";
        }
    }
}

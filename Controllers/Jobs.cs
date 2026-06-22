using FakeStoreLocalAPI.DataBase;
using FakeStoreLocalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jobs : ControllerBase
    {
        FakeStoreDBContext fakeStoreDBContext;
        public Jobs(FakeStoreDBContext fakeStoreDBContext)
        {
            this.fakeStoreDBContext = fakeStoreDBContext;
        }
        [Route("{userId}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.JobDetail>> Get(int userId)
        {
            var items = await fakeStoreDBContext.JobDetail.Where(j => j.UserId == userId).ToListAsync();
            return items;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(JobDetail model)
        {
            fakeStoreDBContext.JobDetail.Add(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return Ok(model.Id);
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(JobDetail model)
        {
            fakeStoreDBContext.JobDetail.Update(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public bool Delete(int id)
        {
            var model = fakeStoreDBContext.JobDetail.FirstOrDefault(j => j.Id == id);
            if (model == null)
                return false;
            fakeStoreDBContext.JobDetail.Remove(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return true;
            else
                return false;
        }
    }
}

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
        [Route("{userId}")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.JobDetail>> Get(int userId)
        {
            using var dbContext = new FakeStoreDBContext();
            var items = await dbContext.JobDetail.Where(j => j.UserId == userId).ToListAsync();
            return items;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(JobDetail model)
        {
            using var dbContext = new FakeStoreDBContext();
            dbContext.JobDetail.Add(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return Ok(model.Id);
            else
                return BadRequest();
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update(JobDetail model)
        {
            using var dbContext = new FakeStoreDBContext();
            dbContext.JobDetail.Update(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public bool Delete(int id)
        {
            using var dbContext = new FakeStoreDBContext();
            var model = dbContext.JobDetail.FirstOrDefault(j => j.Id == id);
            if (model == null)
                return false;
            dbContext.JobDetail.Remove(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return true;
            else
                return false;
        }
    }
}

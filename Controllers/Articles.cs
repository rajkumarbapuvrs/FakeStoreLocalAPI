using FakeStoreLocalAPI.DataBase;
using FakeStoreLocalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Articles : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Article>> Get()
        {
            using var dbContext = new FakeStoreDBContext();
            var items = await dbContext.Article
                .ToListAsync();
            return items;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(Article model)
        {
            using var dbContext = new FakeStoreDBContext();
            dbContext.Article.Add(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }
        [HttpPut]
        [Authorize]
        public IActionResult Update(Article model)
        {
            using var dbContext = new FakeStoreDBContext();
            dbContext.Article.Update(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public string Delete(int id)
        {
            using var dbContext = new FakeStoreDBContext();
            var model = dbContext.Article.FirstOrDefault(a => a.Id == id);
            if (model == null)
                return "Article not found";
            dbContext.Article.Remove(model);
            int ret = dbContext.SaveChanges();
            if (ret == 1)
                return "Article deleted successfully";
            else
                return "Failed to delete article";
        }
    }
}

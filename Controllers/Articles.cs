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
        FakeStoreDBContext fakeStoreDBContext;
        public Articles(FakeStoreDBContext fakeStoreDBContext)
        {
            this.fakeStoreDBContext = fakeStoreDBContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Models.Article>> Get()
        {
            var items = await fakeStoreDBContext.Article
                .ToListAsync();
            return items;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(Article model)
        {
            fakeStoreDBContext.Article.Add(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }
        [HttpPut]
        [Authorize]
        public IActionResult Update(Article model)
        {
            fakeStoreDBContext.Article.Update(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public string Delete(int id)
        {
            var model = fakeStoreDBContext.Article.FirstOrDefault(a => a.Id == id);
            if (model == null)
                return "Article not found";
            fakeStoreDBContext.Article.Remove(model);
            int ret = fakeStoreDBContext.SaveChanges();
            if (ret == 1)
                return "Article deleted successfully";
            else
                return "Failed to delete article";
        }
    }
}

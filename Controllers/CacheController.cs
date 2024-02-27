using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisTest.DataAccess.Context;
using RedisTest.DataAccess.Entities;
using RedisTest.Model;
using RedisTest.Services;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace RedisTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly NorthwindContext _context;
        public CacheController(ICacheService cacheService,
            NorthwindContext context)
        {
            this._cacheService = cacheService;
            this._context = context;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> Get()
        {
            //return Ok("cache123");
            var cacheData = _cacheService.GetData<IEnumerable<User>>("users");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return Ok(cacheData);
            }

            cacheData = await _context.User.ToListAsync();
            var expirtyTime = DateTime.Now.AddHours(10);
            _cacheService.SetData<IEnumerable<User>>("users", cacheData, expirtyTime);

            return Ok(cacheData);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            var addUsers = await _context.User.AddAsync(user);
            var expirtyTime = DateTime.Now.AddSeconds(10);

            _cacheService.SetData<User>($"User{addUsers.Entity.Id}", addUsers.Entity, expirtyTime);
            await _context.SaveChangesAsync();

            return Ok(addUsers.Entity);
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(i => i.Id == userId);
            if (user != null)
            {
                _context.User.Remove(user);
                _cacheService.RemoveData($"User{user.Id}");
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("ClearCache")]
        public async Task<IActionResult> ClearCache()
        {
            await _cacheService.ClearCache();
            return NoContent();
        }


        [HttpGet("GetCache/{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult)NotFound() : Ok(value);

            //List<User> user = new List<User>();
            //using (var context = new NorthwindContext())
            //{
            //    user = (from users in context.User
            //            select users).ToList();
            //}
            //return Ok(user);
        }

        [HttpPost("SetCache")]
        public async Task<IActionResult> SetCacheValue(KeyValueModel request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }

        [HttpPost("RemoveCache/{key}")]
        public async Task<IActionResult> RemoveCache([FromRoute] string key)
        {
            var value = await _cacheService.RemoveCacheValueAsync(key);
            return Ok(value);
        }
    }
}

using Core.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDirectoryService _directoryService;
        private readonly IContactService _contactService;
        private readonly IReportService _reportService;
        private readonly CacheHelper _cacheHelper;

        public APIController(IDistributedCache distributedCache, IDirectoryService directoryService, IContactService contactService, IReportService reportService, CacheHelper cacheHelper)
        {
            _distributedCache = distributedCache;
            _directoryService = directoryService;
            _contactService = contactService;
            _reportService = reportService;
            _cacheHelper = cacheHelper;
        }

        [Route("Directories")]
        [HttpGet]
        public async Task<IActionResult> GetDirectories()
        {
            string cacheKey = CacheHelper.DirectoriesKey;
            var encodedDirectories = await _distributedCache.GetAsync(cacheKey);
            if (encodedDirectories != null)
            {
                var decode = Encoding.UTF8.GetString(encodedDirectories);
                return Content(decode, "application/json");
            }
            else
            {
                var directories = await _directoryService.GetDirectoriesAsync();
                string jsonString = JsonConvert.SerializeObject(directories, Formatting.Indented);
                if (directories != null)
                {
                    encodedDirectories = Encoding.UTF8.GetBytes(jsonString);
                    var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                    await _distributedCache.SetAsync(cacheKey, encodedDirectories, options);
                }
                return Content(jsonString, "application/json");
            }
        }

        [Route("Directory")]
        [HttpGet]
        public async Task<IActionResult> GetDirectory(Directory entity)
        {
            var directory = await _directoryService.GetDirectoryAsync(entity.Uuid);
            string jsonString = JsonConvert.SerializeObject(directory, Formatting.Indented);
            if (directory == null)
                return BadRequest("No matching records were found.");
            return Content(jsonString, "application/json");
        }

        [Route("CreateDirectory")]
        [HttpPost]
        public async Task<IActionResult> CreateDirectory(Directory entity)
        {
            var directory = await _directoryService.AddDirectoryAsync(entity);
            string jsonString = JsonConvert.SerializeObject(directory, Formatting.Indented);
            return Content(jsonString, "application/json");
        }

        [Route("InactiveDirectory")]
        [HttpPatch]
        public async Task<IActionResult> InactiveDirectory(Directory entity)
        {
            var directory = await _directoryService.GetDirectoryAsync(entity.Uuid);
            if (directory == null)
                return BadRequest("No matching records found.");

            directory.IsActive = false;
            await _directoryService.UpdateDirectoryAsync(directory);
            string jsonString = JsonConvert.SerializeObject(directory, Formatting.Indented);
            return Content(jsonString, "application/json");
        }

        [Route("UpdateDirectory")]
        [HttpPatch]
        public async Task<IActionResult> UpdateDirectory(Directory entity)
        {
            var directory = await _directoryService.UpdateDirectoryAsync(entity);
            if (directory == null)
                return BadRequest("An error occured.");
            string jsonString = JsonConvert.SerializeObject(directory, Formatting.Indented);
            return Content(jsonString, "application/json");
        }

        [Route("DeleteDirectory")]
        [HttpPost]
        public async Task<IActionResult> DeleteDirectory(Directory entity)
        {
            bool result = await _directoryService.DeleteDirectoryAsync(entity.Uuid);
            if (result)
                return Content(JsonConvert.SerializeObject(new { message = "Deletion is successful." }), "application/json");
            else
                return BadRequest(JsonConvert.SerializeObject(new { error = true, message = "Deletion is failed." }));
        }

        [Route("AddContact")]
        [HttpPost]
        public async Task<IActionResult> AddContact(Contact entity)
        {
            return Content(JsonConvert.SerializeObject(await _contactService.AddContactAsync(entity), Formatting.Indented), "application/json");
        }

        [Route("InactiveContact")]
        [HttpPatch]
        public async Task<IActionResult> InactiveContact(Contact entity)
        {
            return Content(JsonConvert.SerializeObject(await _contactService.InactiveContactAsync(entity), Formatting.Indented), "application/json");
        }

        [Route("Reporter")]
        [HttpGet]
        public async Task<IActionResult> Reporter()
        {
            return Content(JsonConvert.SerializeObject(await _reportService.ReporterAsync(), Formatting.Indented), "application/json");
        }
    }
}
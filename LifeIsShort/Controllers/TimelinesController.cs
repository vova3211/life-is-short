using LifeIsShort.Domain.Entities.Timelines;
using LifeIsShort.Models.Timelines;
using LifeIsShort.Mongodb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LifeIsShort.Controllers
{
    [Authorize]
    public class TimelinesController : Controller
    {
        private readonly Repository _repository;

        public TimelinesController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var timelines = await (await _repository.GetCollection<Timeline>().FindAsync(Builders<Timeline>.Filter.Eq(x => x.UserId, User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value),
                options: new FindOptions<Timeline, TimelineViewModel>()
                {
                    Sort = Builders<Timeline>.Sort.Descending(x => x.UpdatedAt)
                })).ToListAsync();

            return View(timelines);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimelineInputModel model)
        {
            if (ModelState.IsValid)
            {
                var timeline = new Timeline()
                {
                    MaxYears = model.MaxYears,
                    UserId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Birthday = model.Birthday,
                    Type = model.Type,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _repository.GetCollection<Timeline>().InsertOneAsync(timeline);

                return RedirectToAction("View", "Timelines", new { Id = timeline.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var timeline = await _repository.GetCollection<Timeline>().Find(x => x.Id == id && x.UserId == User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value).FirstOrDefaultAsync();

            if (timeline == null)
            {
                // 404
            }

            return View(timeline);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TimelineInputModel model)
        {
            if (ModelState.IsValid)
            {
                var timeline = new Timeline()
                {
                    MaxYears = model.MaxYears,

                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _repository.GetCollection<Timeline>().InsertOneAsync(timeline);

                return RedirectToAction("View", "Timelines", new { Id = timeline.Id });
            }

            return View(model);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.GetCollection<Timeline>().DeleteOneAsync(Builders<Timeline>.Filter.And(
                Builders<Timeline>.Filter.Eq(x => x.Id, id),
                Builders<Timeline>.Filter.Eq(x => x.UserId, User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value)
                ));

            return RedirectToAction("index", "Timelines");
        }
    }
}

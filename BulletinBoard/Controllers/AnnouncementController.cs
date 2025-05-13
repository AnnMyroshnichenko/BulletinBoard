using BulletinBoard.DAL;
using BulletinBoard.Models;
using BulletinBoardMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly BulletinBoardDbContext _context;

    
        public AnnouncementController(BulletinBoardDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var announcements = _context.Announcements.ToList();
                if (announcements.Count == 0)
                {
                    return NotFound("Announcements not avilable");
                }
                return Ok(announcements);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var announcement = _context.Announcements.Find(id);
                if (announcement == null)
                {
                    return NotFound($"Product details not found with id {id}");
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Announcement announcement)
        {
            try
            {
                _context.Add(announcement);
                _context.SaveChanges();
                return Ok("Announcement created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Announcement model) 
        {
            if (model == null || model.Id == 0)
            {
                if (model == null)
                {
                    return BadRequest("Announcement data is invalid");
                }
                else if (model.Id == 1)
                {
                    return BadRequest($"Announcement Id {model.Id} is invalid");
                }
            }

            try
            {
                var announcement = _context.Announcements.Find(model.Id);
                if (announcement == null)
                {
                    return BadRequest($"Announcement not found with id {model.Id}");
                }

                announcement.Title = model.Title;
                announcement.Description = model.Description;
                announcement.Category = model.Category;
                announcement.SubCategory = model.SubCategory;
                announcement.Status = model.Status;
                _context.SaveChanges();
                return Ok("Announcement details updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound($"Announcement not found with id {id}");
            }
            _context.Announcements.Remove(announcement);
            _context.SaveChanges();
            return Ok("Announcement deleted");
        }
    }
}

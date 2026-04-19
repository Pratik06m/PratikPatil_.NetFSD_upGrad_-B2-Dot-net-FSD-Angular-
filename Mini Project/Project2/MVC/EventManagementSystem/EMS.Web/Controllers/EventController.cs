using EMS.DAL.Models;
using EMS.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepo;

        public EventController(IEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _eventRepo.GetAll();
                return View(events);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error loading events";
                return View(new List<EventDetails>());
            }
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventDetails model)
        {
            try
            {
                // Custom Validation
                if (model.EventDate <= DateTime.Now)
                {
                    ModelState.AddModelError("EventDate", "Event date must be in the future");
                }

                if (ModelState.IsValid)
                {
                    model.EventId = Guid.NewGuid();

                    await _eventRepo.Insert(model);
                    await _eventRepo.Save();

                    TempData["Success"] = "Event created successfully!";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Something went wrong while creating event";
                return View(model);
            }
        }

        // GET: Edit
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var eventData = await _eventRepo.GetById(id);

            if (eventData == null)
            {
                return NotFound();
            }

            return View(eventData);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventDetails model)
        {
            try
            {
                if (model.EventDate <= DateTime.Now)
                {
                    ModelState.AddModelError("EventDate", "Event date must be in the future");
                }

                if (ModelState.IsValid)
                {
                    await _eventRepo.Update(model);
                    await _eventRepo.Save();

                    TempData["Success"] = "Event updated successfully!";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error updating event";
                return View(model);
            }
        }

        // GET: Delete (Confirmation Page)
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var eventData = await _eventRepo.GetById(id);

            if (eventData == null)
            {
                return NotFound();
            }

            return View(eventData);
        }

        // POST: Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _eventRepo.Delete(id);
                await _eventRepo.Save();

                TempData["Success"] = "Event deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Error"] = "Error deleting event";
                return RedirectToAction("Index");
            }
        }
    }
}
using AppUi.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepo;
        private readonly ISessionRepository _sessionRepo;

        public HomeController(IEventRepository eventRepo, ISessionRepository sessionRepo)
        {
            _eventRepo = eventRepo;
            _sessionRepo = sessionRepo;
        }

        public async Task<IActionResult> Index(string? category)
        {
            var categories = (await _eventRepo.GetCategoriesAsync()).ToList();
            var events = string.IsNullOrEmpty(category)
                ? await _eventRepo.GetActiveAsync()
                : await _eventRepo.GetByCategoryAsync(category);

            var vm = new HomeViewModel
            {
                Categories = categories,
                SelectedCategory = category,
                Events = events.Select(e => new EventViewModel
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    EventCategory = e.EventCategory,
                    EventDate = e.EventDate,
                    Description = e.Description,
                    Status = e.Status,
                    Sessions = e.Sessions.Select(s => new SessionViewModel
                    {
                        SessionId = s.SessionId,
                        SessionTitle = s.SessionTitle,
                        SessionStart = s.SessionStart,
                        SessionEnd = s.SessionEnd
                    }).ToList()
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> EventDetails(Guid id)
        {
            var ev = await _eventRepo.GetWithSessionsAsync(id);
            if (ev == null) return NotFound();

            var vm = new EventViewModel
            {
                EventId = ev.EventId,
                EventName = ev.EventName,
                EventCategory = ev.EventCategory,
                EventDate = ev.EventDate,
                Description = ev.Description,
                Status = ev.Status,
                Sessions = ev.Sessions.Select(s => new SessionViewModel
                {
                    SessionId = s.SessionId,
                    SessionTitle = s.SessionTitle,
                    Description = s.Description,
                    SessionStart = s.SessionStart,
                    SessionEnd = s.SessionEnd,
                    SessionUrl = s.SessionUrl,
                    SpeakerName = s.Speaker?.SpeakerName
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> SessionDetails(Guid id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null) return NotFound();

            var vm = new SessionViewModel
            {
                SessionId = session.SessionId,
                EventId = session.EventId,
                SessionTitle = session.SessionTitle,
                Description = session.Description,
                SessionStart = session.SessionStart,
                SessionEnd = session.SessionEnd,
                SessionUrl = session.SessionUrl,
                SpeakerName = session.Speaker?.SpeakerName,
                EventName = session.Event?.EventName
            };

            return View(vm);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

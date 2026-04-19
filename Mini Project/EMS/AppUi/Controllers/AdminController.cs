using AppUi.Models;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppUi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEventRepository _eventRepo;
        private readonly ISessionRepository _sessionRepo;
        private readonly ISpeakerRepository _speakerRepo;
        private readonly IParticipantEventRepository _participantRepo;
        private readonly IUserRepository _userRepo;

        public AdminController(IEventRepository eventRepo, ISessionRepository sessionRepo,
            ISpeakerRepository speakerRepo, IParticipantEventRepository participantRepo,
            IUserRepository userRepo)
        {
            _eventRepo = eventRepo;
            _sessionRepo = sessionRepo;
            _speakerRepo = speakerRepo;
            _participantRepo = participantRepo;
            _userRepo = userRepo;
        }

        private IActionResult RequireAdmin()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("AdminLogin", "Account");
            return null!;
        }

        // ─── Dashboard ──────────────────────────────────────────────────────────

        public async Task<IActionResult> Dashboard()
        {
            var check = RequireAdmin(); if (check != null) return check;

            var events = (await _eventRepo.GetAllAsync()).ToList();
            var users = (await _userRepo.GetAllAsync()).ToList();
            var vm = new AdminDashboardViewModel
            {
                TotalEvents = events.Count,
                ActiveEvents = events.Count(e => e.Status == "Active"),
                TotalSessions = (await _sessionRepo.GetAllAsync()).Count(),
                TotalSpeakers = (await _speakerRepo.GetAllAsync()).Count(),
                TotalParticipants = users.Count(u => u.Role == "Participant")
            };
            return View(vm);
        }

        // ─── Events ─────────────────────────────────────────────────────────────

        public async Task<IActionResult> Events()
        {
            var check = RequireAdmin(); if (check != null) return check;
            var events = await _eventRepo.GetAllAsync();
            var vm = events.Select(e => new EventViewModel
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
                    SessionTitle = s.SessionTitle
                }).ToList()
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            var check = RequireAdmin(); if (check != null) return check;
            return View(new EventViewModel { EventDate = DateTime.Now.AddDays(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;

            if (model.EventDate <= DateTime.Now)
                ModelState.AddModelError("EventDate", "Event Date must be in the future.");

            if (!ModelState.IsValid) return View(model);

            var ev = new EventDetails
            {
                EventName = model.EventName,
                EventCategory = model.EventCategory,
                EventDate = model.EventDate,
                Description = model.Description,
                Status = model.Status
            };
            await _eventRepo.AddAsync(ev);
            TempData["Success"] = "Event created successfully.";
            return RedirectToAction("Events");
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            var ev = await _eventRepo.GetByIdAsync(id);
            if (ev == null) return NotFound();
            var vm = new EventViewModel
            {
                EventId = ev.EventId,
                EventName = ev.EventName,
                EventCategory = ev.EventCategory,
                EventDate = ev.EventDate,
                Description = ev.Description,
                Status = ev.Status
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(EventViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;

            if (model.EventDate <= DateTime.Now)
                ModelState.AddModelError("EventDate", "Event Date must be in the future.");

            if (!ModelState.IsValid) return View(model);

            var ev = new EventDetails
            {
                EventId = model.EventId,
                EventName = model.EventName,
                EventCategory = model.EventCategory,
                EventDate = model.EventDate,
                Description = model.Description,
                Status = model.Status
            };
            await _eventRepo.UpdateAsync(ev);
            TempData["Success"] = "Event updated successfully.";
            return RedirectToAction("Events");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            await _eventRepo.DeleteAsync(id);
            TempData["Success"] = "Event deleted.";
            return RedirectToAction("Events");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEventStatus(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            await _eventRepo.ToggleStatusAsync(id);
            return RedirectToAction("Events");
        }

        // ─── Sessions ───────────────────────────────────────────────────────────

        public async Task<IActionResult> Sessions(Guid? eventId)
        {
            var check = RequireAdmin(); if (check != null) return check;

            var sessions = eventId.HasValue
                ? await _sessionRepo.GetByEventIdAsync(eventId.Value)
                : await _sessionRepo.GetAllAsync();

            var vm = sessions.Select(s => new SessionViewModel
            {
                SessionId = s.SessionId,
                EventId = s.EventId,
                SessionTitle = s.SessionTitle,
                Description = s.Description,
                SessionStart = s.SessionStart,
                SessionEnd = s.SessionEnd,
                SessionUrl = s.SessionUrl,
                SpeakerName = s.Speaker?.SpeakerName,
                EventName = s.Event?.EventName
            }).ToList();

            ViewBag.FilteredEventId = eventId;
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> CreateSession(Guid? eventId)
        {
            var check = RequireAdmin(); if (check != null) return check;
            var events = await _eventRepo.GetAllAsync();
            ViewBag.Events = new SelectList(events, "EventId", "EventName", eventId);
            var vm = new SessionViewModel
            {
                EventId = eventId ?? Guid.Empty,
                SessionStart = DateTime.Now.AddDays(1),
                SessionEnd = DateTime.Now.AddDays(1).AddHours(1)
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSession(SessionViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;

            if (model.SessionStart >= model.SessionEnd)
                ModelState.AddModelError("SessionEnd", "End time must be after Start time.");

            if (!ModelState.IsValid)
            {
                var events = await _eventRepo.GetAllAsync();
                ViewBag.Events = new SelectList(events, "EventId", "EventName", model.EventId);
                return View(model);
            }

            var session = new SessionInfo
            {
                EventId = model.EventId,
                SessionTitle = model.SessionTitle,
                Description = model.Description,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd,
                SessionUrl = model.SessionUrl,
                SpeakerId = model.SpeakerId == Guid.Empty ? null : model.SpeakerId
            };
            await _sessionRepo.AddAsync(session);
            TempData["Success"] = "Session created successfully.";
            return RedirectToAction("Sessions");
        }

        [HttpGet]
        public async Task<IActionResult> EditSession(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null) return NotFound();

            var events = await _eventRepo.GetAllAsync();
            var speakers = await _speakerRepo.GetAllAsync();
            ViewBag.Events = new SelectList(events, "EventId", "EventName", session.EventId);
            ViewBag.Speakers = new SelectList(speakers, "SpeakerId", "SpeakerName", session.SpeakerId);

            var vm = new SessionViewModel
            {
                SessionId = session.SessionId,
                EventId = session.EventId,
                SessionTitle = session.SessionTitle,
                Description = session.Description,
                SessionStart = session.SessionStart,
                SessionEnd = session.SessionEnd,
                SessionUrl = session.SessionUrl,
                SpeakerId = session.SpeakerId
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSession(SessionViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;

            if (model.SessionStart >= model.SessionEnd)
                ModelState.AddModelError("SessionEnd", "End time must be after Start time.");

            if (!ModelState.IsValid)
            {
                var events = await _eventRepo.GetAllAsync();
                var speakers = await _speakerRepo.GetAllAsync();
                ViewBag.Events = new SelectList(events, "EventId", "EventName", model.EventId);
                ViewBag.Speakers = new SelectList(speakers, "SpeakerId", "SpeakerName", model.SpeakerId);
                return View(model);
            }

            var session = new SessionInfo
            {
                SessionId = model.SessionId,
                EventId = model.EventId,
                SessionTitle = model.SessionTitle,
                Description = model.Description,
                SessionStart = model.SessionStart,
                SessionEnd = model.SessionEnd,
                SessionUrl = model.SessionUrl,
                SpeakerId = model.SpeakerId == Guid.Empty ? null : model.SpeakerId
            };
            await _sessionRepo.UpdateAsync(session);
            TempData["Success"] = "Session updated successfully.";
            return RedirectToAction("Sessions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSession(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            await _sessionRepo.DeleteAsync(id);
            TempData["Success"] = "Session deleted.";
            return RedirectToAction("Sessions");
        }

        [HttpGet]
        public async Task<IActionResult> AssignSpeaker(Guid sessionId)
        {
            var check = RequireAdmin(); if (check != null) return check;
            var session = await _sessionRepo.GetByIdAsync(sessionId);
            if (session == null) return NotFound();

            var speakers = await _speakerRepo.GetAllAsync();
            if (!speakers.Any())
            {
                TempData["Error"] = "No speakers found. Please add speakers first.";
                return RedirectToAction("Speakers");
            }

            var vm = new AssignSpeakerViewModel
            {
                SessionId = sessionId,
                SessionTitle = session.SessionTitle,
                EventName = session.Event?.EventName ?? "",
                SpeakerId = session.SpeakerId ?? Guid.Empty,
                Speakers = speakers.Select(s => new SpeakerViewModel
                {
                    SpeakerId = s.SpeakerId,
                    SpeakerName = s.SpeakerName
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignSpeaker(AssignSpeakerViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;
            await _sessionRepo.AssignSpeakerAsync(model.SessionId, model.SpeakerId);
            TempData["Success"] = "Speaker assigned successfully.";
            return RedirectToAction("Sessions");
        }

        // ─── Speakers ────────────────────────────────────────────────────────────

        public async Task<IActionResult> Speakers()
        {
            var check = RequireAdmin(); if (check != null) return check;
            var speakers = await _speakerRepo.GetAllAsync();
            var vm = speakers.Select(s => new SpeakerViewModel
            {
                SpeakerId = s.SpeakerId,
                SpeakerName = s.SpeakerName
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateSpeaker()
        {
            var check = RequireAdmin(); if (check != null) return check;
            return View(new SpeakerViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpeaker(SpeakerViewModel model)
        {
            var check = RequireAdmin(); if (check != null) return check;
            if (!ModelState.IsValid) return View(model);

            var speaker = new SpeakersDetails { SpeakerName = model.SpeakerName };
            await _speakerRepo.AddAsync(speaker);
            TempData["Success"] = "Speaker added successfully.";
            return RedirectToAction("Speakers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSpeaker(Guid id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            await _speakerRepo.DeleteAsync(id);
            TempData["Success"] = "Speaker removed.";
            return RedirectToAction("Speakers");
        }
    }
}

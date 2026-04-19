using AppUi.Models;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppUi.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IEventRepository _eventRepo;
        private readonly IParticipantEventRepository _participantRepo;
        private readonly ISessionRepository _sessionRepo;

        public ParticipantController(IEventRepository eventRepo,
            IParticipantEventRepository participantRepo, ISessionRepository sessionRepo)
        {
            _eventRepo = eventRepo;
            _participantRepo = participantRepo;
            _sessionRepo = sessionRepo;
        }

        private string? CurrentEmail => HttpContext.Session.GetString("UserEmail");
        private bool IsParticipant => HttpContext.Session.GetString("Role") == "Participant";

        private IActionResult RequireParticipant(string? returnUrl = null)
        {
            if (!IsParticipant)
                return RedirectToAction("Login", "Account", new { returnUrl });
            return null!;
        }

        public async Task<IActionResult> Dashboard()
        {
            var check = RequireParticipant(); if (check != null) return check;

            var registrations = await _participantRepo.GetByParticipantAsync(CurrentEmail!);
            var vm = new ParticipantDashboardViewModel
            {
                UserName = HttpContext.Session.GetString("UserName") ?? "",
                Email = CurrentEmail!,
                RegisteredEvents = registrations.Select(r => new RegisteredEventViewModel
                {
                    RegistrationId = r.ID,
                    EventId = r.EventId,
                    EventName = r.Event?.EventName ?? "",
                    EventCategory = r.Event?.EventCategory ?? "",
                    EventDate = r.Event?.EventDate ?? DateTime.MinValue,
                    Status = r.Event?.Status ?? "",
                    IsAttended = r.IsAttended,
                    Sessions = (r.Event?.Sessions ?? new List<SessionInfo>()).Select(s => new SessionViewModel
                    {
                        SessionId = s.SessionId,
                        SessionTitle = s.SessionTitle,
                        SessionStart = s.SessionStart,
                        SessionEnd = s.SessionEnd,
                        SessionUrl = s.SessionUrl
                    }).ToList()
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterEvent(Guid eventId)
        {
            if (!IsParticipant)
                return RedirectToAction("Login", "Account",
                    new { returnUrl = Url.Action("EventDetails", "Home", new { id = eventId }) });

            if (await _participantRepo.IsRegisteredAsync(CurrentEmail!, eventId))
            {
                TempData["Error"] = "You are already registered for this event.";
                return RedirectToAction("EventDetails", "Home", new { id = eventId });
            }

            var reg = new ParticipantEventDetails
            {
                ParticipantEmailId = CurrentEmail!,
                EventId = eventId,
                IsAttended = false
            };
            await _participantRepo.RegisterAsync(reg);
            TempData["Success"] = "Successfully registered for the event!";
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnregisterEvent(Guid registrationId)
        {
            var check = RequireParticipant(); if (check != null) return check;
            await _participantRepo.UnregisterAsync(registrationId);
            TempData["Success"] = "Unregistered from event.";
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAttendance(Guid registrationId, bool isAttended)
        {
            var check = RequireParticipant(); if (check != null) return check;
            await _participantRepo.MarkAttendanceAsync(registrationId, isAttended);
            TempData["Success"] = "Attendance updated.";
            return RedirectToAction("Dashboard");
        }
    }
}

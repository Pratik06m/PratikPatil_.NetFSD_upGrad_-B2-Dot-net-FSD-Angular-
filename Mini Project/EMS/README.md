# upGrad EMS — Setup & Run Guide

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB, Express, or full) 
- Visual Studio 2022 (or VS Code with C# extension)

---

## Step 1 — Clone / Open the Project

Open `EMS.sln` in Visual Studio 2022.

---

## Step 2 — Configure Connection String

Edit `AppUi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EMSDatabase;Trusted_Connection=True;"
  }
}
```

For SQL Server Express use:
```
Server=.\\SQLEXPRESS;Database=EMSDatabase;Trusted_Connection=True;
```

For full SQL Server use:
```
Server=YOUR_SERVER;Database=EMSDatabase;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

---

## Step 3 — Run EF Core Migrations

Open **Package Manager Console** in Visual Studio:

```
Tools → NuGet Package Manager → Package Manager Console
```

Set **Default project** to `DAL`. Then run:

```powershell
# Set AppUi as startup project (right-click → Set as Startup Project)

Add-Migration InitialCreate -Project DAL -StartupProject AppUi
Update-Database -Project DAL -StartupProject AppUi
```

Or using .NET CLI from the `EMS` root folder:

```bash
dotnet ef migrations add InitialCreate --project DAL --startup-project AppUi
dotnet ef database update --project DAL --startup-project AppUi
```

This creates the `EMSDatabase` and seeds the Admin user automatically.

---

## Step 4 — Run the Application

Press **F5** in Visual Studio, or:

```bash
cd AppUi
dotnet run
```

---

## Default Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@upgrad.com | Admin@123 |
| Participant | Register at /Account/Register | — |

---

## Navigation

| URL | Description |
|-----|-------------|
| `/` | Home — Browse events |
| `/Account/Register` | Participant sign-up |
| `/Account/Login` | Participant login |
| `/Account/AdminLogin` | Admin login |
| `/Admin/Dashboard` | Admin dashboard |
| `/Admin/Events` | Manage events |
| `/Admin/Sessions` | Manage sessions |
| `/Admin/Speakers` | Manage speakers |
| `/Participant/Dashboard` | Participant dashboard |

---

## Project Structure

```
EMS/
├── EMS.sln
├── DAL/                          ← Class Library (Data Access Layer)
│   ├── DAL.csproj
│   ├── Models/
│   │   ├── UserInfo.cs
│   │   ├── EventDetails.cs
│   │   ├── SpeakersDetails.cs
│   │   ├── SessionInfo.cs
│   │   └── ParticipantEventDetails.cs
│   ├── DataAccess/
│   │   └── EMSDbContext.cs       ← DbContext + Fluent API + Seed
│   └── Repository/
│       ├── IUserRepository.cs
│       ├── UserRepository.cs
│       ├── IEventRepository.cs
│       ├── EventRepository.cs
│       ├── ISpeakerRepository.cs
│       ├── SpeakerRepository.cs
│       ├── ISessionRepository.cs
│       ├── SessionRepository.cs
│       ├── IParticipantEventRepository.cs
│       └── ParticipantEventRepository.cs
│
└── AppUi/                        ← ASP.NET Core MVC Application
    ├── AppUi.csproj
    ├── Program.cs                ← DI, Session, EF, Middleware
    ├── appsettings.json
    ├── Controllers/
    │   ├── HomeController.cs     ← Public pages
    │   ├── AccountController.cs  ← Login, Register, Logout
    │   ├── AdminController.cs    ← All admin CRUD
    │   └── ParticipantController.cs
    ├── Models/
    │   └── ViewModels.cs         ← All ViewModels
    ├── Views/
    │   ├── _ViewImports.cshtml
    │   ├── _ViewStart.cshtml
    │   ├── Shared/
    │   │   ├── _AdminLayout.cshtml
    │   │   ├── _ParticipantLayout.cshtml
    │   │   └── _ValidationScriptsPartial.cshtml
    │   ├── Home/
    │   │   ├── Index.cshtml
    │   │   ├── EventDetails.cshtml
    │   │   └── SessionDetails.cshtml
    │   ├── Account/
    │   │   ├── Login.cshtml
    │   │   ├── AdminLogin.cshtml
    │   │   └── Register.cshtml
    │   ├── Admin/
    │   │   ├── Dashboard.cshtml
    │   │   ├── Events.cshtml
    │   │   ├── CreateEvent.cshtml
    │   │   ├── EditEvent.cshtml
    │   │   ├── Sessions.cshtml
    │   │   ├── CreateSession.cshtml
    │   │   ├── EditSession.cshtml
    │   │   ├── AssignSpeaker.cshtml
    │   │   ├── Speakers.cshtml
    │   │   └── CreateSpeaker.cshtml
    │   └── Participant/
    │       └── Dashboard.cshtml
    └── wwwroot/
        └── css/
            ├── site.css
            └── admin.css
```

---

## Architecture

```
Browser
  ↓
ASP.NET Core MVC (AppUi)
  ↓  Controllers → ViewModels → Views
  ↓  Dependency Injection
Repository Interfaces (DAL)
  ↓
Repository Implementations (DAL)
  ↓
EF Core DbContext
  ↓
SQL Server Database
```

## Key Design Decisions
- **Repository Pattern**: All DB access is abstracted behind interfaces, injected via DI
- **Code First**: Schema generated from entity classes + Fluent API configuration
- **Session-based auth**: Role stored in session (Admin / Participant)
- **Two Layouts**: `_AdminLayout` and `_ParticipantLayout` for separate UX
- **Auto-migration**: `db.Database.Migrate()` in `Program.cs` runs on startup
- **Seeded Admin**: `admin@upgrad.com / Admin@123` inserted via `HasData`

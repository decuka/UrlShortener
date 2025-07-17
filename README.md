# URL Shortener – Inforce Test Assignment

This repository contains the solution for the **Inforce** technical test task.  The goal is to implement a simple URL shortening service with a clean back-end API and an Angular front-end.

---
## Features
* Create an 8-character short code for any valid URL
* View and delete links (owner or **Admin** role only)
* JWT authentication with role-based access (User / Admin)
* Public “About” page, editable only by admins
* Unit tests for both C# and TypeScript code

---
## Tech Stack
| Layer | Technology |
|-------|-------------|
| Back-end | ASP.NET Core 8, EF Core 8, **SQL Server** |
| Front-end | Angular 18, TypeScript, RxJS |
| Auth | JWT Bearer + ASP.NET Identity |
| Tests | xUnit + Moq (C#) / Karma + Jasmine (TS) |

---
## Prerequisites
* .NET SDK 8.0 or later
* SQL Server (LocalDB or full instance)
* Node 18+
* npm (bundled with Node) and Google Chrome for headless tests

---
## Getting Started (development mode)
```bash
# 1. Restore back-end packages
cd UrlShortener
 dotnet restore

# 2. Install front-end packages
cd ClientApp
npm install

# 3. Run API and Angular dev server (SpaProxy starts ng serve automatically)
cd ..   # back to UrlShortener
dotnet run
# API → https://localhost:7248
# SPA → http://localhost:44111 (fixed port set in proxy)
```
Port scheme in dev:
* **7248** – Kestrel API (`/api/*`).
* **44111** – Angular CLI (always the same; configured in `package.json` and proxy script).

---
## Running Tests
### Back-end
```bash
dotnet test UrlShortener.Tests/UrlShortener.Tests.csproj
```

### Front-end
```bash
cd UrlShortener/ClientApp
npm run test -- --watch=false --browsers=ChromeHeadless
```

---
### Database
Entity Framework Core migrations are applied automatically at application startup, so you do **not** need to run `dotnet ef database update` after cloning the repository. 
```

---
## API Documentation (Swagger)
The back-end API exposes an interactive **Swagger / OpenAPI** specification.

Run the application in development mode and open:

```text
https://localhost:7248/swagger
```

Here you can inspect all endpoints, their request/response schemas, and even execute calls directly from the browser.

1. Click **Authorize** and paste a JWT access token (e.g. obtained from `/api/auth/login`) prefixed with `Bearer`.
2. Use **Try it out** on any operation to send a request.
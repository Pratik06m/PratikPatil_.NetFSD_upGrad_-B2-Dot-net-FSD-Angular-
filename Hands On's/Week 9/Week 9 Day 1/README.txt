Secure Contact Management API - Week 9 Day 1

Run steps:
1. Open solution folder in Visual Studio 2022 or later.
2. Restore NuGet packages.
3. Run the project.
4. Swagger opens automatically.

Seeded users:
Admin -> admin@example.com / Admin@123
User  -> user@example.com / User@123

Test flow:
1. POST /api/auth/login
2. Copy token
3. Click Authorize in Swagger
4. Enter: Bearer <token>
5. Test secured APIs

Role access:
Admin = full CRUD
User  = read-only

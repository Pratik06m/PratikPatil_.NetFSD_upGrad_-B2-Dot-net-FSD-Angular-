Week-9 Day-2 - Contact Management API

This project combines:
1. In-Memory Caching using IMemoryCache
2. Paging using Skip and Take
3. Built-in Rate Limiting using Fixed Window Limiter

Endpoints:
1. GET /api/contacts/cached
   - Returns all contacts using cache
   - Cache expires after 60 seconds

2. GET /api/contacts/cached/{id}
   - Returns contact by ID using cache
   - Cache expires after 60 seconds

3. GET /api/contacts?pageNumber=1&pageSize=5
   - Returns paged contacts
   - Includes metadata

Rate Limiting:
- Applied on ContactsController
- Allows 5 requests per 60 seconds per client IP
- 6th request returns 429

Notes:
- Uses EF Core InMemory database to simulate DB
- Repository prints console messages to show DB hits
- Service layer handles cache logic

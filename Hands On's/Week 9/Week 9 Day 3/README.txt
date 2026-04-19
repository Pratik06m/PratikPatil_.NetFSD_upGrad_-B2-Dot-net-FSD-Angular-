Week-9 Day-3 Microservices Solution

Projects:
- ContactService
- CategoryService
- ApiGateway

Ports:
- ContactService: https://localhost:7001
- CategoryService: https://localhost:7002
- ApiGateway: https://localhost:7000

Databases:
- ContactService uses Week9Day3_ContactDb
- CategoryService uses Week9Day3_CategoryDb

How to run:
1. Open solution in Visual Studio.
2. Set multiple startup projects:
   - ContactService
   - CategoryService
   - ApiGateway
3. Run all three.
4. Call only through gateway:
   - https://localhost:7000/api/contacts
   - https://localhost:7000/api/categories

Important rules:
- CategoryId in Contact is only a plain reference value.
- No foreign key.
- No validation with CategoryService.
- No service-to-service communication.
- Each microservice owns its own database.

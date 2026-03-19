# webapp for 2025-2026 spring semester

**Axora - Lab workflow app**

Backend and admin ui is implemented in AspNet Core MVC, 
using standard libraries (EF Core, Identity, Postgres DB).  
Design patterns - N-Tier, Solid. Domain, DAL, BLL and UI layers.

# Requirements
Here are requirements for the entire project. It answers questions like:
- What this project does?
- What technical needs it has to fulfill?
- What practices are going to be implemented?

## General project requirements for successful homework
- Clean/Onion Architecture
- REST API layer
- JWT authentication
- working client application
- Project proposal has to be approved - was approved on 18.03
- Evidence
  - source code
  - API documentation (Swagger)
  - key diagrams (DB/architecture/auth flow)
  - short AI usage log
- Should be done end of week 8 but is fine if gets it done by 09.04

## Practices to keep an eye on
### General truths
- YAGNI (You Ain’t Gonna Need It)
  - YAGNI advises against implementing features until they are necessary, to avoid unnecessary complexity and maintain focus on immediate requirements.
- KISS (Keep It Simple, Stupid)
  - KISS encourages simplicity in design and development, favoring straightforward solutions over complex ones to improve understandability and maintainability.
- DRY (Don’t Repeat Yourself)
  - DRY promotes code reusability by avoiding duplication of code or logic, leading to cleaner, more maintainable codebases.
- Single Responsibility Principle (SRP)
  - "Each class should have only one reason to change."
- Open/Closed Principle (OCP)
  - "Software entities should be open for extension but closed for modification."
- Liskov Substitution Principle (LSP)
  - "Subtypes should be substitutable for their base types without altering the correctness of the program."
    If code works with the base type, every derived type must honor that contract.
- Interface Segregation Principle (ISP)
  - "Clients should not be forced to depend on interfaces they don't use."
- Dependency Inversion Principle (DIP)
  - "High-level modules should not depend on low-level modules; both should depend on abstractions."
    This is the principle that directly enables DI and Clean Architecture.
### Implementation practices
- Use of repositories 
  - to have data access in one place
- Unit of work
  - Tracks all changes you make during a business transaction and commits them in one atomic operation.
- DTO (Data Transfer Objects)
  - Dumb objects with no logic — just properties. Their job: carry data between layers without leaking internal structure.
- Mapper
  - Something has to convert between DTO's and layers.
- Dependency Injection
  - Don't create your dependencies — demand them through the constructor. Let someone else figure out what concrete implementation to hand you.
### Architectural patterns
- MVC Model-View-Controller

#Ideas for this document
- Break down requirements to smaller tasks in table
- Map UX/Views to backend endponts - “Button X → Calls Endpoint Y → Affects Entity Z”
- Write down things idk ask AI and document so later ik what I learned and to refer it for defence

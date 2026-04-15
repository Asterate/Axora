# webapp for 2025-2026 spring semester

**Axora - Lab workflow app**

Backend and admin ui is implemented in AspNet Core MVC, 
using standard libraries (EF Core, Identity, Postgres DB).  
Design patterns - N-Tier, Solid. Domain, DAL, BLL and UI layers.

# Plan
| Step                              | Description                                                                                                                    | Status                            |
|-----------------------------------|--------------------------------------------------------------------------------------------------------------------------------|-----------------------------------|
| Requirements                      | Write down initial project requirements that the project needs to be complete                                                  | Done                              |
| UX                                | Design the UX flow in Figma and define all the buttons and the definitions and needs have to work with requirements            | Done                              |
| Basic requirements implementation | Make all the systems that needs to be applied everywhere. Like repositories, authentication etc                                | In progress                       |
| Views buttons                     | Put all the buttons that are needed to the views and implement them one by one so when one works effectevly others can as well | In progress                       |
| Create logs from scaffolded views | To be able to see what is in the database and delete and add as needed to make implementation easier                           | Done                              |
| Add BLL                           | Buttons to do more than CRUD                                                                                                   | Not needed yet                    |
| Add UI                            | For it to have eye pleasing but professional design                                                                            | not needed yet                    |
| Translations                      | Automatic tranlation between eng and et                                                                                        | Initial setup done                |
| idor                              | Seperate data from users                                                                                                       | not started                       |
| swagger                           | api web endpoints                                                                                                              | up and running                    |
| rest api                          | in backend defined rest api for swagger                                                                                        | need to add more and fix problems |

After each step would be needed to check if I am following the best practices with AI and manually.

# Requirements
Here are requirements for the entire project. It answers questions like:
- What this project does?
- What technical needs it has to fulfill?
- What practices are going to be implemented?

## General project requirements for inspiration
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
- Should be done end of week 8 but is fine if gets it done by 16.04

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
  - Controller should be thin
    - receive input
    - call a service
    - pick a view
  - Views should be dumb
    - No business logic in .cshtml. No if (user.Role == "Admin" && user.Subscription.Tier > 2) in Razor. Compute that in the service, put a bool ShowAdminPanel on the ViewModel.
  - ViewModels are NOT entities
    - This is the DTO concept applied to the View layer. The View gets exactly what it needs to render — nothing more. Entity goes in, ViewModel comes out, the controller/service does the mapping.
- N-Tier
  - Split your application into horizontal layers, each with a single responsibility. Each layer only talks to the one directly below it.
  - Web -> BLL -> DAL
- Clean / Onion Architecture
  -  flip the dependency direction. Instead of everything depending on DAL, everything depends on a shared abstractions project (or multiple). Infrastructure (EF, DB) becomes a plugin — it depends inward, not the other way around.
  - Domain references nothing. It's the center. Everything else depends on it. Infrastructure is a leaf — remove EF Core, swap to Dapper, nobody else notices.
- Modular Monolith
  - Clean boundaries like microservices, deployment simplicity of a monolith. Split later if you actually need to
  - Modules never reference each other's internals
    - Module A doesn't touch Module B's entities, repositories, or DbContext.
    - Separate DbContexts per module
  - Coupling vs Cohesion — the two metrics that matter
    - Coupling = how many modules depend on each other's internals.
    - Cohesion = how related the stuff inside a module is.
    - The goal: low coupling between modules, high cohesion within modules.
  - The migration path

## Each view requirements
### Log in/index
- Needs to have log in field
- Registration
- forgot password
- general news and updates
### Home
- Different projects
- Updates within associated organisation
### Project view
- Project schedule and timeline
- Project experiments
- Project data
- Project Schedule
### Experiment planning view
- Tools
- Mind map of experiments and their subtasks
### Experiment view
- Tools
- Task at hand and interactive molecule tools
- Short descriptions of data
- Predictions of experiments
### Documentation view
- CRUD documentation
- well and easy to read list of documents
- Update panel of recent adds
- Change to write and commit changes
### Analysis view
- Charts
- Tables
- Comparisons of different results, experiments
### Equipment, materials view
- List of available equipment, material
- Can schedule usage
## Upload legacy work like Excel
- upload button
- edit and assign location

# Swagger
Under api button in footer there is swagger part where can currently do in client:
- authentication
- crud experiment, project

### Ideas for this document
- Break down requirements to smaller tasks in table
- Map UX/Views to backend endponts - “Button X → Calls Endpoint Y → Affects Entity Z”
- Write down things idk ask AI and document so later ik what I learned and to refer it for defence

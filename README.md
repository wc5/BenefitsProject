# BenefitsProject
### _A somewhat ordered list of considerations/concerns/questions:_
### Overall
- This could have been accomplished with minimal thought, design, or lines of code, but I figured that wasn't a good idea...
- The challenge was vague to the point that in real life I would ask for a ton of clarification regarding requirements such as:
  - Does the data need to be persisted?
- I decided to make opinionated assumptions and to design the project in a way that incorporated my full-stack abilities.
- I tried to balance KISS/not over-engineering with showcasing what I can do.
- This is my normal implementation of Onion or Clean Architecture (give or take).
- This project was small enough to be a single ASP.NET Core WebAPI project, but using separate projects provides clear boundaries.
- I'm used to using ASP.NET Core MVC, but I wanted to use this opportunity to design a Web API and use React.
- I spent infinitely more time on `BenefitsProject.Core` and `BenefitsProject.Core.UnitTests` than I did on `BenefitsProject.webclient`.

### BenefitsProject.Core
- Using a `BaseEntity` base class to track things such as:
  - `CreatedAt`
  - `CreatedBy`
  - `ModifiedAt`
  - `ModifiedBy`
  - `IsActive`
- Using an `IPerson` interface for standardization
- Choosing a proper Id type (_e.g._ `int` vs. `long` vs `guid`)
- Would it be better to build custom validation than have a dependency on something like FluentValidation in the project core?
- Regarding requirements/expectations:
  - Is it better to calculate and have more properties/data points (_e.g._ `NetSalary`) or just let the client app handle it?
  - What should the unique constraint be for an `Employee`?
    - `BusinessEmailAddress`
    - `EmployeeIdentificationNumber`
  - What should the unique constraint be for a `Dependent`?
  - Should we track `DateOfBirth` of a `Dependent`?
    - If a `Dependent` is marked as `Dependent.Child`, can they be too old?
    - If so, what about `Dependent`s that are too old but meet other requirements?
    - What would we do with `Dependent` that is too old, mark them as `IsActive = false`?
  - How should the system handle two employees that are married or are Parent/Child?
    - Should they be listed as one another's `Dependent`?
    - If they have children, which `EmployeeId` gets used for the `Dependent`?
  
### BenefitsProject.Infrastructure
- I know some EF users (and maybe others) are anti-repository pattern.
- I would have `Delete_Async()` set `IsActive = false` instead of hard deleting any data.
- Would we use migrations in production?
- Should we use a micro ORM like Dapper instead of EF?
- I would not use Sqlite in production for this.
- Is setting the precision on money columns appropriate, common, useful, etc?
- Should we require existence on the database column and the Domain Entity property, or only in one place?
  
### BenefitsProject.WebApi
- We would definitely need Authentication (_e.g._ using a JWTs)
- We would also need to setup up Authorization:
  - We might need multiple access levels.
  - I'd have one attribute on the `EmployeeController`.
  - I'd have attributes on some or all of the `EmployeeController` `Actions`.
- We would need a proper `ConnectionString` and use environment variables.
- We would problably want to implement `ILogger`.
- We would need to ensure a proper CORS setup.
- If we had multiple controllers, I would probably want to implement CQRS using MediatR.
- I would not have the app launch to the Swagger UI in production.

### BenefitsProject.webclient
- I've never seen production React code, and it probably shows.
- This design could be vastly improved.
- Each of the DTOs from `BenefitsProject.WebApi` should be adapted as Interfaces (I think this can be automated using swagger?).
- I would probably have a single file containing all API calls.
- I need to display validation errors or bad responses from the API to the client user.
- Update/Edit/Put isn't implemented in the client
- Adding dependents isn't implemented in the client.
- The project definitely needs some tests.

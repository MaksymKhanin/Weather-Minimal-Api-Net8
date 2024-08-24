# Weather Minimal Api

A simple Weather Forecast CRUD api. Allows to get weather forecast for specific date, add new weather forecast, clear all.

# Tech stack

.Net 8, Minimal Api, Docker

# A bit of explanation

Exception handling: Result pattern + Global Exception Handler
Structured logging
DDD: I am a DDD practioner so I created 2 value objects, with validation inside. I create them by calling Static Factory methods. All props are restricted to private set, Constructor is also private to restrict creation other than calling Create method, which has validation inside. Further planning to make aggreagate to handle all validations in one place from aggregate root.
Also for validation I implemented meditor validation pipeline with FluentValidator inside.
.http files from where you can call Api, which also serves as documentation.

# Architecture

Hexagonal architecture: Api, Business, Core, Persistance projects.

For simplicity I use InMemoryStorage for development, but later maybe will add MongoDb in docker container.
For Error handling implemented Result pattern which returns success or error objects.
If I have time , will add tests to Tests project.

# Tests

As minimal apis don`t have the built-in model validation, we have to pay double attention to validation and tests.
For that purpose I follow the "Always valid domain" approach with 2 step validation. First step happens in mediator pipeline. Second step - inside Value object itself during creation of domain object. So that domain objects will always be valid.
To test it I crated Api.Tests project, where test successfull case, and invalid models. I created a BadRequestJsonTestCases file which contains invalid json requests.

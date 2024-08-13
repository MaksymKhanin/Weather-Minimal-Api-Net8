# Weather Minimal Api

A simple Weather Forecast CRUD api. Allows to get weather forecast for specific date, add new weather forecast, clear all.

# Tech stack

.Net 8, Minimal Api, Docker

# A bit of explanation

Exception handling: Result pattern + Global Exception Handler
Structured logging
DDD - DTO, Domain Objects, Entities
Domain objects implement Static Factory methods. All props are restricted to private set, private ctor. Factory method.
Automapper
.http files from where you can call Api, which also serves as documentation.

# Architecture

Hexagonal architecture: Api, Business, Core, Persistance projects.

For simplicity I use InMemoryStorage for development, but later maybe will add MongoDb in docker container.
For Error handling implemented Result pattern which returns success or error objects.
If I have time , will add tests to Tests project.

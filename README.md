# **Cats**
======================================================================

This repository contains the code to generate list of cats (by owner's gender).

This projects can be invoked in a few ways:
1. Cats api  Route - /api/cats
2. React app Route - /cats
2. Swagger - Route - /swagger


## Structure:
+ `Models` stores the model for the service
+ `Library` contains the services that perform the actual business operation
+ `UnitTests` contains unit tests for the Library services
+ `FunctionalTests` contains integration tests for the api
+ `Api` contains web apis that generate list of cats by owner's gender
+ `App` renders the view in the ui fetching data from the api


## Getting Startedapi
These instructions will serve as a guide to run and test the project in your local machine.
- Download the code to your local machine
- Build the application. ( You may need to  restore packages)

### Prerequisites
- .NET Core 2.1



### Running Locally

- Run the Cats app by pressing ctrl + F5 nad browse to /cats route. The sample file path is configured in appsettings.
- The api is integrated with swagger. You can also call the api via swagger ui

### Testing Locally

- Run unit test and functional tests using Resharper or an xUnit Runner
# GenericStore

## Description  
GenericStore is a lightweight starting point for building applications using **Domain-Driven Design (DDD)**, **object-oriented programming (OOP)**, and **SOLID principles**. It includes a clear layered structure and a test project to help you build maintainable, testable, and scalable software.

## Key Features  

- Clean architecture with separated layers: **Domain**, **Application**, **Infrastructure**, **API**, and **Tests**  
- Encapsulated domain logic: entities, value objects, aggregates, and business rules live in the Domain layer  
- Adherence to **SOLID** and **OOP** best practices — for low coupling, high cohesion, and easy extensibility  
- Built-in automated testing (unit or integration tests) to catch regressions and ensure correct behavior  

## Typical Project Structure  

/GenericStore\
/GenericStore.Domain # Domain entities, value objects, business logic\
/GenericStore.Application # Use-cases, application services, orchestration\
/GenericStore.Infrastructure # Persistence, data access, external adapters\
/GenericStore.Api # REST/API layer (controllers, endpoints)\
/GenericStore.Test # Automated tests (unit / integration)\
GenericStore.sln 

## Live demo

The project is deployed on a Raspberry Pi 3B+ on a Docker server with two containers for each API project and a PostgreSQL database also hosted in another container. 

http://lucashome1424.duckdns.org:5000/swagger/index.html

http://lucashome1424.duckdns.org:5001/swagger/index.html

# Main solution file

## Getting Started  

1. Clone the repo:  
   ```bash
   git clone https://github.com/Lucaso1424/GenericStore.git
   cd GenericStore
Open the solution in your preferred .NET-compatible IDE (Visual Studio, VS Code, Rider, etc.).

Configure your database connection or any required external dependencies (if using a persistent store).

Restore packages and build the solution.

Running Tests
Run all tests to verify functionality:

dotnet test

How to Contribute
Create a feature or bug-fix branch: feature/xyz or bugfix/xyz

Implement your changes and add corresponding tests

Ensure the architecture and SOLID/OOP principles are preserved

Submit a pull request with a clear description of your changes

### License
Copyright 2025 Lucas Padilla Hidalgo

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

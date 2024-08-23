# 🚀 CleanArchCQRSMediatorAPI

This repository serves as a reference for building a RESTful API using the .NET Framework while adhering to the principles of Clean Architecture and Clean Code as introduced by Uncle Bob.

Inspired by https://github.com/MihiraBandara/Skeleton.

## 🗂️ Project Structure

The folder structure is organized as follows:

```plaintext
.
├── .editorconfig
├── .gitattributes
├── .gitignore
├── API/
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── CleanArchCQRSMediatorAPI.API.csproj
│   ├── CleanArchCQRSMediatorAPI.API.csproj.user
│   ├── Dockerfile
│   ├── Exceptions/
│   ├── Extensions/
│   ├── Interfaces/
│   ├── Module/
│   ├── Program.cs
│   └── Properties/
├── Core/
│   ├── Application/
│   └── Domain/
├── Infrastructure/
│   ├── Identity/
│   ├── Persistence/
│   └── Utility/
├── Tests/
│   ├── Application/
├── readme.md
├── docker-compose.yaml
└── CleanArchCQRSMediatorAPI.sln
```

## 🏗️ Solution Overview

This solution is structured with Clean Architecture in mind, dividing responsibilities across multiple projects:

- **API**: Handles the presentation layer, API routes, and configurations.
- **Core**: Contains the business logic (Application) and domain models (Domain).
- **Infrastructure**: Manages data access, identity, and utility services.
- **Tests**: Houses unit and integration tests.

## 🔧 Key Features

- **📊 Logging**: Serilog, Azure Application Insights, and Elasticsearch are integrated for comprehensive logging.
- **🔐 Authentication**: Implemented using ASP.NET Core Identity.
- **⚡ Caching**: In-memory caching and Redis caching are both supported.
- **🖥️ Presentation API**: A minimal API is structured with Swagger documentation for easy testing and debugging.
- **🗄️ Persistence**: The main branch uses MSSQL as the database provider. Future updates will introduce PostgreSQL support.
- **🔄 CQRS and Mediator Patterns**:
  - **CQRS**: Separates read and write operations into distinct models to optimize performance, scalability, and security. Commands and queries are handled separately to ensure that the read side can scale independently from the write side.
  - **Mediator Pattern**: Facilitates communication between components by centralizing requests and responses. This pattern is implemented using the MediatR library to handle commands and queries, promoting loose coupling and adherence to single responsibility principles.

## 🚀 Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/vpgits/CleanArchCQRSMediatorAPI.git
   ```

2. Configure the MSSQL Database. Obtain a connection string for the Database. Perform the migration.
   - There are 2 Contexts to migrate from, `LibraryDbContext` and `AuthDbContext`.
   - Perform the necessary migrations using `dotnet-cli` and update the database ,or try out the self migration script in the `program.cs`(not recommended).
3. Build the solution using Visual Studio or your preferred IDE.
4. Run the API using the provided Docker configuration or directly through the IDE. (Prefer using the IDE and running the supplementary services through the provided docker-compose)

## 🚧 Improvements & Contributions

Feel free to open an issue or submit a pull request for any improvements or new features you'd like to contribute!

## ⚠️ Notes

- **Dockerization**: Be cautious when running the application with Elasticsearch in Docker, as it has a longer cold start, potentially causing the docker-compose process to hang in Visual Studio.
- Docker configurations for Elasticsearch, Kibana, and Redis are provided.
- **ElasticSearch and Kibana**: Refer to their documentation on configuring a user for Kibana to log the results into a presentable GUI

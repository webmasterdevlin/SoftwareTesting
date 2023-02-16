# Writing Automated Tests

- Note: Create an instance of the database before running the tests
- Name the database "TestDB"

## Installing Database using Docker

- https://github.com/webmasterdevlin/docker-compose-database
- install docker client for your OS
- Install Azure Data Studio
- docker commands of each database are located on each Readme.md file

## Commands

- dotnet tool install --global dotnet-ef
- dotnet ef migrations add InitialCreate
- dotnet ef database update

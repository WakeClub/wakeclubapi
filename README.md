## Installation of Dotnet Code
- Download Dotnet 8.0 SDK
- Clone code into repository

## Configuration
- appsettings.json/appsettings.Development.json file:
```
"DefaultConnection": "Server=127.0.0.1,1433;Database=<DB NAME>;User Id=<User Id>;Password=<Password>;TrustServerCertificate=True;"
```

## Setting up Docker SQL image
- https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&pivots=cs1-bash
- Use Azure Data Studio (mac) or SQL Server Management Studio (Windows) to connect to the database

## DB Migrations
- https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

## Running the API
- On running the code using your IDE, a swagger window should pop up on your browser. You can use that to test the API endpoints.

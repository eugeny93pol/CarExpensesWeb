# CarExpensesWeb

## Pre-requirements

* Visual Studio 2019
* .NET Core SDK
* SQL Server
* Node.js

## How To Run

* Open solution in Visual Studio 2019
* Insert your Database connection string in file `CE.WebAPI/appsettings.json`:

      {
        "ConnectionStrings": {
              "DbConnection": "<Insert connection string here>"
        },
        ...
      }

* Open `Package Manager Console` and run `Update Database`
* Set `CE.WebAPI` project as Startup Project 
* Run `npm install` in the client project directory (`CE.ClientApp`)
* Build the solution.
* Run the application.
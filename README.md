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

## API Reference

### Private routes

#### Users

##### Admins only

* `GET api/users/` gets array of all users.
* `PUT api/users/{id: long}` updates the user with the specified `id`. 
  Format of request body:

      {
        "name": "user_name",
        "email": "email@example.com",
        "password": "user_password",
        "role": "user_role"
      }

##### Users & admins

* `GET api/users/{id: long}` gets one user with the specified `id`. Users with the role 
  'user' have access only to their own profile. Users with the role 'admin' have access 
  to all profiles.

* `POST api/users/` creates a new user with the role 'user'. Format of request body:

      {
        "name": "user_name",
        "email": "email@example.com",
        "password": "user_password"
      }

* `PATCH api/users/{id: long}` updates partially the user with the specified `id`. 
  Missing or null fields will not be updated. Format of request body:

      {
        "name": "user_name",
        "email": "email@example.com",
        "password": "user_password"
      }

* `DELETE api/users/{id: long}` deletes the user with the specified `id`. Users with the role
  'user' have access only to their own profile. Users with the role 'admin' have access
  to all profiles.
# BulletinBoard Project
The BulletinBoard project is a website for managing announcements. This repository contains two main components:

- BulletinBoardAPI: A RESTful API built with ASP.NET Core to manage announcements.
- BulletinBoardMVC: A web application built with ASP.NET Core MVC to interact with the API and display announcements.

## Setup Instructions

### Clone the Repository:

``git clone https://github.com/yourusername/BulletinBoard.git
cd BulletinBoard``

### Restore NuGet packages:

``dotnet restore``

### Configure Connection String:
Use user secrets for local development

``cd BulletinBoardAPI
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:BulletinBoard:SqlDb" "Server=(localdb)\\MSSQLLocalDB;Database=BulletinBoard;Trusted_Connection=True;MultipleActiveResultSets=true"``

Alternatively, edit appsettings.json:

``{
  "ConnectionStrings": {
    "BulletinBoard:SqlDb": "Server=(localdb)\\MSSQLLocalDB;Database=BulletinBoard;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}``

### Set Up the Database:
Generate and apply migrations

``cd BulletinBoardAPI
dotnet ef migrations add InitialCreate
dotnet ef database update``

This creates the BulletinBoard database with the Announcements table and seeds sample data.

### Run the Application:
Start both projects in Visual Studio (set as multiple startup projects).

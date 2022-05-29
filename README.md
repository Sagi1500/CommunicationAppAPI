# CommunicationAppAPI


# Getting Started
* Make sure MariaDB has been installed. If not, check out this link for downloads information https://mariadb.org/download/
* Clone this repository to your project.
* This application uses MariaDB as a database. In order to use this database, you have to download Pomelo Entity Framework.<br> In order to install it, press on Tools-> Nuget Package Manager -> Packet Manager Console. Use the command: <br> $ Install-Package Pomelo.EntityFrameworkCore.MySql -Version 6.0.1
* Then, install the Microsoft Entity Framework Tools package by entering the following command: <br> $ Install-Package Microsoft.EntityFrameworkcore.Tools -version 6.0.
* Please repeat that install process at the other project.
* At this solution, we will create two databases. At CommunicationApi the DB will use to save users, contacts, and messages. The other one at CommunicationAppApi will save the ratings. At CommunicationApi.
* Finally create the databases:<br>
  &nbsp; a. Navigate to: CommunicationApi\Data\ApplicationContext.cs and change your mariaDB name in connectionString as you wish.<br>
  &nbsp; b. Navigate to: CommunicationAppApi\Data\RatingsContext.cs and change your mariaDB name in connectionString as you wish.<br>
  &nbsp; c. Navigate to: CommunicationApi\Data\ApplicationContext.cs and change your mariaDB password in connectionString as you wish.<br>
  &nbsp; d. Navigate to: CommunicationAppApi\Data\RatingsContext.cs and change your mariaDB password in connectionString as you wish.<br>
  &nbsp; e. Apply the migration by entering on the Package Manager Console: $ update-database.

* In order to run both projects together. Press right-click on the solution and press on "Set startup project.." choose the Multiple startup projects option and change CommunicationApi and CommunicationAppApi to Start on the Action column.ד

At this point, you are ready to start. Press the start button to run the projects.

## CommunicationApi - Web Api project
As the instuctions of this execise requsted we implemented the API using the following controllers.
### Users Controller - http://localhost:7049/api/Users
  - Http GET
  - Http POST
  - Http GET with {id} 
  - Http DELETE with {id}
### Login Controller - http://localhost:7049/api/{id}/Login
  - Http POST
### Contacts Controller - http://localhost:7049/api/{id}/Contacts
  - Http GET
  - Http POST
  - Http GET with {id}
  - Http PUT with {id} 
  - Http DELETE with {id}
### Messages Controller - http://localhost:7049/api/Contacts/{id}/Messages
  - Http GET
  - Http POST
  - Http GET with {id}
  - Http PUT with {id} 
  - Http DELETE with {id}
### Invitation Controller - http://localhost:7049/api/Invitation
  - Http POST
### Transfer Controller - http://localhost:7049/api/Trasfer
  - Http POST

Notice: the URL we mention is the initial URL and might change as requested in the API.

## Services
We created services for User, contact, message, and AppHub services. Each of them has an ApplicationContext file that provides the opportunity to read and write to the database. In addition, AppHub has a dictionary that connects between 2 strings. The key is the connection Id filed and the value is the UserId (username).

## Hubs
We created AppHub and its service in order to provide SignalR communication between the client and the server.

## CommunicationAppApi - ASP.NET MVC project
* You will be using this project if you like to see, update or remove ratings on this app.

### The application has five views

#### Index page: http://localhost:5219/Ratings/Index.
  - On this page, you will see all the existing ratings.
  - Has the option to search for specific rating by the name of editor or description.
  - Has a button to create a new Rating.
  - Has a button for returning to the app.
  - while opening a rating from the list, there is three buttons for Editing, Deleting, or Details for this specific rating.

#### Create page: http://localhost:5219/Ratings/Create.
  - Allows to create new ratings and saves them at the DB.
  - The page will allow you to submit a rating if all the condition met.

#### Details page: http://localhost:5219/Ratings/Details/{id}.
  - Allows seeing all information about the rating his id equals to the value in {id}.
  - Has buttons for moving to edit or the delete page and go back button for going back to the Index page.

#### Delete page: http://localhost:5219/Ratings/Delete/{id}.
  - Allows deleting the rating which his id equals to the value in {id}.
  - Has buttons for moving to the edit or details page and go back button for going back to the Index page.

#### Edit page: http://localhost:5219/Ratings/Edit/{id}.
  - Allows editing the rating which his id equals to the value in {id}.
  - Editing is only approved if the verification conditions are met.
  - Has buttons for moving to delete or details page and go back button for going back to the Index page.


## Databases
## The AppDb database - The database has 3 tables.
#### First table - Users
  - string - Id - the username filed - KEY in this table.
  - string - Password - data type of password.
#### Second table - Messages
  - int Id - message number - KEY in this table.
  - string - UserId 
  - string - ContactId 
  - string - Content 
  - DateTime -  Created
  - bool - Send  
#### Third table - Contacts
  - int - Id - foreign key
  - string - UserId - foreign key
  - string - Name
  - string - Server
  - string - Last 
  - DateTime - LastDate
## The Ratings database - The database has 1 table.
### table - Ratings
  - int - Id - key
  - string - Name
  - int - Rate (with range from 1 to 5)
  - string - Description
  - DateTime - Time





  

# Example React App 

Created for my Nephew to demonstrate .Net Core React App functionality. 

It is made up of 3 main parts
* Directory SolarDataApp\ClientApp > The react client side application code and dependencies. 
* Directory SolarDataApp > The server side C# .Net Core application containing API's, but also hosts the client side react app for serving from the web server. 
* Directory SolarDataApp.Tests > The server side code unit testing project. This tests the majority of functionality in the server side code, so if these tests pass, the server side application is working as per expectations. 

# Setup

You need to create a MariaDb database, and user for the system to use. Either MariaDB or generic MySql will do. 

To create a user, run these commands on mySql command line or other client. 
```
create database SolarDataApp;
grant all on SolarDataApp.* to 'SolarDataApp'@'%' identified by 'somePasswordToChange';
```

Then run this script to create the required database structure and initial data. 
```
{{SolarDataApp/DataAccess/database.sql}}
```

You will need to update the appsettings.json in both C# Projects file with a valid config file. 
`"DatabaseConnString": "Server=localhost;Database=SolarDataApp;Uid=SolarDataApp;Pwd=somePasswordToChange;SslMode=Preferred;",`
Or you can use secrets storage to store the config in a file located in: 
`%APPDATA%\Microsoft\UserSecrets\24fa0558-e29e-49af-b4f7-3e1687c405f8\secrets.json`

# Build

After cloning the repository, you will need to install dependencies. 

```
cd SolarDataApp\ClientApp
npm i
```

# Test

To test the server side C# code using the unit tests, open the solution in Visual Studio. 
Right click on the 'SolarDataApp.Tests' project and click on the 'Run Unit Tests' menu item. 

Then Open in Visual Studio and try 'runing' the solution. Usually F5 will do that. 

There are no tests in the React client side app right now. 

# Editing

You can open the server side code in Visual Studio, however the client React app is a little clunky to edit as VS tries to reformat it. So you are better of editing the client side app in Visual Studio Code. 

There are more references in [Create React App Readme](SolarDataApp/ClientApp/README.md) 
This project was created using the ASP.Net core template creation process as per https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-2.2&tabs=visual-studio
More react documentation can be found at
* https://reactjs.org/
* https://reactjs.org/tutorial/tutorial.html
* https://reactjs.org/docs/getting-started.html

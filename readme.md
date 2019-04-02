# Example React App 

Created for my Nephew to demonstrate .Net Core React App functionality. 

It is made up of 3 main parts
* Directory SolarDataApp\ClientApp > The react client side application code and dependencies. 
* Directory SolarDataApp > The server side C# .Net Core application containing API's, but also hosts the client side react app for serving from the web server. 
* Directory SolarDataApp.Tests > The server side code unit testing project. This tests the majority of functionality in the server side code, so if these tests pass, the server side application is working as per expectations. 

# Test

To test the server side C# code using the unit tests, open the solution in Visual Studio. 
Right click on the 'SolarDataApp.Tests' project and click on the 'Run Unit Tests' menu item. 

# Build

After cloning the repository, you will need to install dependencies. 

```
cd SolarDataApp\ClientApp
npm i
```

Then Open in Visual Studio and try 'runing' the solution. Usually F5 will do that. 

# Editing

You can open the server side code in Visual Studio, however the client React app is a little clunky to edit as VS tries to reformat it. So you are better of editing the client side app in Visual Studio Code. 


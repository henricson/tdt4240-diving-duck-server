# Diving Duck Server

This project contains the server for keeping the Diving Duck high scores. It is written in C# 7, and uses the asp.NET and EF Core framework.

### Setting up the Database

Before running the server, you need to set up the database by updating the `appsettings.Development.json` file. In this file, you will need to specify the connection string to your PostgresDB instance.

### Running the Server

To run the server, you have two options:

#### Option 1: Dotnet CLI

1. Open a terminal or command prompt.
2. Navigate to the root directory of the `tdt4240-diving-duck-server` project.
3. Run the following command to apply the database migrations:

`dotnet ef database update`

4. Run the following command to start the server:

`dotnet run`

This will build and start the server.

#### Option 2: Visual Studio

1. Open the `tdt4240-diving-duck-server` project in Visual Studio for Windows or MacOS.
2. Ensure that the `appsettings.Development.json` file has been updated with the correct database connection string.
3. Use the Visual Studio interface to build and run the server.

Note that if you're using Visual Studio, you may need to set up additional configurations, such as specifying the project to run or the environment to use. However, the general process should be similar to running the server using the Dotnet CLI.

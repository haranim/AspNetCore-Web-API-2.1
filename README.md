# AspNetCore-Web-API-2.1
with JWT Authentication

# Prerequisites
1. .NET Core SDK 2.1 
2. Visual Studio 2017 version 15.9
3. MongoDB

# Features
    - Rest services with various CRUD operations on Mongo DB
    - Authentication using JWT token
    - Swagger UI
    - Gloabl Error Handling with built in middleware
    - supporting Pagination for bulk data
    - Unit testing
   
# Use case
1. Games and Collections can be fetched by anonymous user
2. A user has to register and login to create a session for a valid game ID

# Database Set Up
1. Choose a folder in local system to store database
2. run below command on command shell instance situated in "C:\Program Files\MongoDB\Server\4.0\bin"
   mongod --dbpath <data_directory_path>
3. Open another command instance and run scripts from Database_Script.txt to create basic collections

# API Set up
1. Download source code and execute
2. APIs can be tested on Postman / Swagger

Architecture:
https://github.com/haranim/AspNetCore-Web-API-2.1/blob/master/Architecture.JPG

      

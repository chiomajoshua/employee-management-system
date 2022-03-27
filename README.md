﻿# employee-management-system
An organization has hired you to build an employee management system for them. The application is supposed to allow employees and an admin to use the system. The administrator should create user accounts and the application should allow salary payments.

## Project Structure

    .
    ├── src                                     # Application Source Files.
            ├──app
                    ├── api
    |                   └── EMS.Data            # Employee Management System Data Layer
    
    ├── .gitignore                              # Git ignore.
    ├── README.md                               # This file.
    

#### API Documentation
API documentation is [here](https://{deployedLocation}/swagger) after running the application on visual studio 2022

#### Technologies Used
- dotnet 6


## Getting Started

##### Prerequisites
- dotnet 6 SDK needs to be installed on your machine. See [dotnet 6 Docs](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Visual studio 2022 needs to be installed
- SQL Server Management Studio needs to be installed on you local machine

##### Setting up locally
- Clone with SSH => `git@github.com:chiomajoshua/employee-management-system.git`
- Clone with HTTPS => `https://github.com/chiomajoshua/employee-management-system.git`


##### Running the app on an attached device
- Open project with visual studio
- If Nugets do not restore automatically, right-click on the solution folder and select Restore Nuget Packages
- Build and Run the project.
# EmployeesManagmentAPI
This Job Portal Application is built with .NET 7.0 and utilizes MSSQL to manage data. It enables two distinct roles: Employee and JobOffer.

## Role-Based Access Control

### Employee

When logged in as an Employee, you have access to the following methods:

- Publish your employee job position
- View available job offers but you can't see other employee job positions by employee role
- Filter job offers based on personal interests
- Delete your employee job position

### JobOffer

When logged in as a JobOffer, you have access to the following methods:

- Publish job offers
- Access analytics for your job offers
- Filter job positions based on personal interests
- Delete your jobOffer


## Usage

### Registration

To register a user, ensure that you provide a valid GUID. Registration without a valid GUID will not be accepted. Make sure to follow the specific format and guidelines for the GUID when registering a new user.

Here are 10 randomly generated valid GUIDs:
62046d6a-25c2-4d25-80e7-3a5f2d47f541
98a9b9bf-2f43-4eaa-8c2e-10abbc077c92
efc27b3e-2a16-4c3f-8e67-9713fca1b86e
10bf1f9e-f1b0-4d0c-a7d2-6e50b10c93d4
a95eb64c-3c18-45cf-8b18-83b6c299019e
6f1f0d24-3f7e-4c31-92f0-4d0aefad8935
c2f37384-1c5d-4b1b-883d-6ed6e00b5db4
d1a4a134-c90b-4c09-bc46-9d7b094a37d1
b4c1bbd5-3ea7-4d95-b9ed-27e54a7d1639
7fc1956a-f1d3-4889-9b35-5c3b2ec2a8f6

## Token Validity

The access token provided upon successful authentication is valid for 2 days. After this period, users will need to re-authenticate to obtain a new token.

## Package References

This project uses the following packages:

- **AutoMapper** - version 12.0.1
- **AutoMapper.Extensions.Microsoft.DependencyInjection** - version 12.0.1
- **Microsoft.AspNetCore.Authentication.JwtBearer** - version 7.0.13
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** - version 7.0.13
- **Microsoft.AspNetCore.OpenApi** - version 7.0.11
- **Microsoft.EntityFrameworkCore.SqlServer** - version 7.0.13
- **Microsoft.EntityFrameworkCore.Tools** - version 7.0.13

## Local Host Ports

The application is hosted on the following local host ports:

- HTTP: http://localhost:5235
- HTTPS: https://localhost:7239


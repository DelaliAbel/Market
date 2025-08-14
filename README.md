# MarketWeb

A modular, full-stack e-commerce web application built with .NET 8, Blazor (WebAssembly and Server), and ASP.NET Core. The solution is organized into several projects, each with a clear responsibility, following best practices for maintainability and scalability.

## Solution Structure

- **MarketWeb__Client**:  
  Blazor WebAssembly client application. Handles the user interface, authentication, shopping cart, and communicates with the API for data operations.

- **MarketWeb_Server**:  
  Blazor Server application. Hosts server-side Blazor components, manages authentication, authorization, and provides server-side rendering and services.

- **MarketWeb_API**:  
  ASP.NET Core Web API project. Exposes RESTful endpoints for products, orders, and user accounts. Implements JWT authentication and CORS for secure cross-origin requests.

- **MarketWeb_Business**:  
  Contains business logic and repository implementations for data access and manipulation. Uses AutoMapper for DTO/entity mapping.

- **MarketWeb_DataAccess**:  
  Entity Framework Core data models and `ApplicationDbContext`. Defines entities such as `Product`, `Category`, `OrderHeader`, `OrderDetail`, and `ApplicationUser` (for Identity).

- **MarketWeb_Models**:  
  Data Transfer Objects (DTOs) for API communication and Blazor client consumption. Includes models for products, categories, orders, authentication, and error handling.

- **MarketWeb_Common**:  
  Shared constants and static details (e.g., roles, status strings, local storage keys) used across the solution.

## Key Features

- **Authentication & Authorization**:  
  Uses ASP.NET Core Identity and JWT for secure user management and API access.

- **Product & Category Management**:  
  CRUD operations for products and categories, with support for product pricing and categorization.

- **Order Processing**:  
  Models and endpoints for order headers and details, supporting shopping cart and checkout flows.

- **Blazor WebAssembly Client**:  
  Modern, responsive UI with local storage, authentication state management, and API integration.

- **API Documentation**:  
  Integrated Swagger/OpenAPI for easy API exploration and testing.

- **Repository Pattern**:  
  Clean separation of data access logic using repositories and interfaces.

## How It Works

- The **Blazor WebAssembly client** (`MarketWeb__Client`) interacts with the **API** (`MarketWeb_API`) for all data operations, including authentication, product browsing, and order placement.
- The **API** uses **MarketWeb_Business** for business logic and **MarketWeb_DataAccess** for database operations.
- **MarketWeb_Server** can be used for server-side Blazor rendering or as a host for the application.
- **MarketWeb_Models** and **MarketWeb_Common** provide shared types and constants to ensure consistency across all projects.

## Getting Started

1. **Database**:  
   Update the connection string in your configuration files and run EF Core migrations to set up the database.

2. **Run the API**:  
   Start `MarketWeb_API` to expose the backend endpoints.

3. **Run the Client**:  
   Start `MarketWeb__Client` for the Blazor WebAssembly frontend.

4. **(Optional) Run the Server**:  
   Start `MarketWeb_Server` for server-side Blazor or as a fallback host.

## Technologies Used

- .NET 8
- Blazor WebAssembly & Server
- ASP.NET Core Identity & JWT
- Entity Framework Core
- AutoMapper
- Swagger/OpenAPI
- Blazored.LocalStorage

## Project Highlights

- Modular, layered architecture for maintainability.
- Secure authentication and role-based authorization.
- Modern SPA frontend with Blazor.
- Clean separation of concerns with DTOs, repositories, and services.

---

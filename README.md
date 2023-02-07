# Products-Catalog

Project created by Luis Miguel Morales Alvarez for Fullstack Developer QualIT -Technical Assessment

Stack used:

Backend
* .NET 7
* ASP.NET Core: 7.0.2
* Entity Framework Core: 7.0.2
* ASP.NET Identity Core: 7.0.2
* ASP.NET Authentication JwtBearer: 7.0.2
* SQL Server: 16.0.1000.6
* Serilog: 2.12.0
* Serilog Extensions Hosting: 5.0.1
* Serilog Console Sink: 4.1.0
* Serilog File Sink: 5.0.0

Frontend (/Products-Catalog/client folder)
* NextJs: 13.1.6
* Axios: 1.3.2
* Bootstrap React: 5.2.3
* React: 18.2.0

Routes
* / -> Catalog screen
* /signin -> Sign In screen
* /signup -> Sign Up screen
* /new-products -> Create product screen

API Endpoints
* POST /api/Users/signin -> User sign-in endpoint
* POST /api/User/signup -> User sign-up endpoint
* GET /api/Products -> Product catalog endpoint, get all the products
* POST /api/Products -> Create new product

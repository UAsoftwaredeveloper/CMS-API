# TravelUCMS Solution

A modular .NET 6 solution for managing travel-related operations, including CMS, hotel, activity, transfer, and reporting services. The solution is organized into multiple projects and libraries for scalability and maintainability.

---

## Table of Contents

1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
3. [Project Structure](#project-structure)
4. [Installation](#installation)
5. [Usage](#usage)
6. [Configuration](#configuration)
7. [API Reference](#api-reference)
8. [Testing](#testing)
9. [Contributing](#contributing)
10. [License](#license)

## Introduction
## Projects Overview

### 1. CMS (Web API)
- **Purpose:** Main entry point for API endpoints, configuration, and authentication.
- **Key Features:**
  - JWT-based authentication and authorization (configurable in `appsettings.json`)
  - Centralized configuration for multiple databases (CMS, TMM, HotelAdmin, ActivityAdmin, TransferAdmin)
  - Swagger UI for API documentation and testing
  - In-memory caching with feature toggles
  - Optional background job worker
  - Global error and 404 handling middleware
  - Response compression and HTTP client factory
  - Controllers for:
    - Flight Enquiries
    - Hotel Deals
    - Template Details
    - Quotation Email Support

### 2. Cms.Services
- **Purpose:** Business logic and service layer for various modules.
- **Key Services:**
  - **TMM Services:**
    - `EnqueryPageDetailsService`
    - `DynamicDestinationEnquiryService`
    - `CruiseBookingTransactionDetailsService`
    - `BookingTransactionDetailsService`
    - `PriceTrackingCustomerInfoService`
    - `GroupTravelFlightEnqueryDetailsService`
    - `FlightsEnquiryService`
    - `AllInOneReportService`
    - `VideoConsulationService`
    - `CruiseSearchDetailsService`
  - **HotelAdmin Services:**
    - `HotelBookingDetailsService`
  - **ActivityAdmin Services:**
    - `ActivityBookingDetailsService`
    - `ActivitySearchLogsService`
  - **TransferAdmin Services:**
    - `CarBookingTransactionDetailsService`
  - **Utilities:**
    - `PaginatedList` for pagination support
    - `MappingProfile` for AutoMapper configurations

### 3. DataManager
- **Purpose:** Data access layer and entity definitions.
- **Key Components:**
  - Database context classes for TMM, HotelAdmin, ActivityAdmin, and TransferAdmin
  - Entity classes for booking, enquiry, and transaction details

### 4. CMS.Repositories
- **Purpose:** Repository pattern implementation for data access abstraction.
- **Key Features:**
  - TMM repositories (e.g., `DynamicDestinationEnquiryRepository`)
  - Dependency registration extensions

---

## Configuration

- **Database Connections:**  
  Multiple connection strings for different modules in `appsettings.json`.
- **JWT Settings:**  
  Configurable secret, issuer, audience, and token duration.
- **Caching:**  
  Options to enable/disable caching for template details and holiday packages.
- **Job Worker:**  
  Toggle background job worker via configuration.

---

## Getting Started

1. **Clone the repository**
2. **Update `appsettings.json`** with your environment-specific connection strings and JWT settings.
3. **Build the solution** using Visual Studio 2022.
4. **Run the CMS project** to start the API.
5. **Access Swagger UI** at `/swagger` for API documentation and testing.

---

## Technologies Used

- .NET 6
- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- JWT Authentication

---

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## Author

- Name: Umesh Aggarwal <aggarwalumesh1@gmail.com>
- Email: aggarwalumesh1@gmail.com
- Organization: None

---

## License

This project is licensed under the MIT License.  
See the [LICENSE](LICENSE) file for details.

---

**Note:** This solution contains sensitive connection strings in `appsettings.json`. Ensure you secure your credentials and never commit real secrets to public repositories.


# Directory Structure

The directory structure for the solution follows a modular and layered architecture pattern:

- **Client:** Contains the client-side code (if applicable).
- **Controllers:** ASP.NET Core MVC controllers for handling HTTP requests.
- **Services:** Business logic and service layer.
- **Repositories:** Data access layer implementing the repository pattern.
- **DbContexts/Database:** Entity Framework Core database contexts and entities.

# API Reference

| Method | Endpoint                              | Description                                 | Auth Required |
|--------|---------------------------------------|---------------------------------------------|:------------:|
| POST   | /api/FlightsEnquiry                   | Search for flight offers                    |      ✔️      |
| GET    | /api/FlightsEnquiry/{id}              | Get details of a specific flight enquiry    |      ✔️      |
| DELETE | /api/FlightsEnquiry/{id}              | Delete a flight enquiry                     |      ✔️      |
| POST   | /api/FlightBooking                    | Book a flight                               |      ✔️      |
| GET    | /api/FlightBooking/{id}               | Get booking details                         |      ✔️      |
| DELETE | /api/FlightBooking/{id}               | Cancel a booking                            |      ✔️      |
| PUT    | /api/Cache/TemplateDetails            | Enable/disable and configure template cache |      ✔️      |
| GET    | /api/Cache/TemplateDetails            | Get current caching configuration           |      ✔️      |
| POST   | /api/FlightsEnquiry/LowCost           | Search for low-cost flight options          |      ✔️      |

> **Note:** All endpoints require JWT Bearer authentication.

## Flights Enquiry

### `POST /api/FlightsEnquiry`

#### Request
POST /api/FlightsEnquiry
Authorization: Bearer {token}
**Content-Type:** `application/json`

```json
{
  "origin": "JFK",
  "destination": "LAX",
  "departureDate": "2025-08-10"
}
```
#### Response
HTTP/1.1 200 OK
**Content-Type:** `application/json`

```json
{
  "flightOffers": [
    {
      "id": "1",
      "airline": "Airline A",
      "departureTime": "2025-08-10T08:00:00",
      "arrivalTime": "2025-08-10T11:00:00",
      "price": 199.99,
      "currency": "USD"
    },
    {
      "id": "2",
      "airline": "Airline B",
      "departureTime": "2025-08-10T09:00:00",
      "arrivalTime": "2025-08-10T12:00:00",
      "price": 249.99,
      "currency": "USD"
    }
  ]
}
```
### `GET /api/FlightsEnquiry/{id}`

#### Request
GET /api/FlightsEnquiry/1
Authorization: Bearer {token}
#### Response
HTTP/1.1 200 OK
**Content-Type:** `application/json`

```json
{
  "id": "1",
  "airline": "Airline A",
  "departureTime": "2025-08-10T08:00:00",
  "arrivalTime": "2025-08-10T11:00:00",
  "price": 199.99,
  "currency": "USD",
  "segments": [
    {
      "departureAirport": "JFK",
      "arrivalAirport": "LAX",
      "airline": "Airline A",
      "flightNumber": "AA123",
      "departureTime": "2025-08-10T08:00:00",
      "arrivalTime": "2025-08-10T11:00:00",
      "duration": "3h"
    }
  ]
}
```
### `DELETE /api/FlightsEnquiry/{id}`

#### Request
DELETE /api/FlightsEnquiry/1
Authorization: Bearer {token}
#### Response
HTTP/1.1 204 No Content
## Flight Booking

### `POST /api/FlightBooking`

#### Request
POST /api/FlightBooking
Authorization: Bearer {token}
**Content-Type:** `application/json`

```json
{
  "flightId": "1",
  "passengerDetails": [
    {
      "firstName": "John",
      "lastName": "Doe",
      "dateOfBirth": "1990-01-01",
      "gender": "M"
    }
  ],
  "paymentInfo": {
    "cardNumber": "4111111111111111",
    "cardExpiry": "12/25",
    "cardCvc": "123"
  }
}
```
#### Response
HTTP/1.1 201 Created
Content-Type: application/json
Location: /api/FlightBooking/1

{
  "id": "1",
  "flightId": "1",
  "status": "Confirmed",
  "totalPrice": 199.99,
  "currency": "USD",
  "paymentStatus": "Paid"
}
```
### `GET /api/FlightBooking/{id}`

#### Request
GET /api/FlightBooking/1
Authorization: Bearer {token}
#### Response
HTTP/1.1 200 OK
**Content-Type:** `application/json`

```json
{
  "id": "1",
  "flightId": "1",
  "passengerDetails": [
    {
      "firstName": "John",
      "lastName": "Doe",
      "dateOfBirth": "1990-01-01",
      "gender": "M"
    }
  ],
  "paymentInfo": {
    "cardNumber": "************1111",
    "cardExpiry": "12/25",
    "cardCvc": "***"
  },
  "status": "Confirmed",
  "totalPrice": 199.99,
  "currency": "USD",
  "paymentStatus": "Paid"
}
```
### `DELETE /api/FlightBooking/{id}`

#### Request
DELETE /api/FlightBooking/1
Authorization: Bearer {token}
#### Response
HTTP/1.1 204 No Content
## Caching

### `PUT /api/Cache/TemplateDetails`

#### Request
PUT /api/Cache/TemplateDetails
Authorization: Bearer {token}
**Content-Type:** `application/json`

```json
{
  "enabled": true,
  "expirationInMinutes": 60
}
```
#### Response
HTTP/1.1 204 No Content
### `GET /api/Cache/TemplateDetails`

#### Request
GET /api/Cache/TemplateDetails
Authorization: Bearer {token}
#### Response
HTTP/1.1 200 OK
**Content-Type:** `application/json`

```json
{
  "enabled": true,
  "expirationInMinutes": 60
}

## Flights Enquiry with Low-Cost Filter

### `POST /api/FlightsEnquiry/LowCost`

#### Request
POST /api/FlightsEnquiry/LowCost
Authorization: Bearer {token}
**Content-Type:** `application/json`

```json
{
  "origin": "JFK",
  "destination": "LAX",
  "departureDate": "2025-08-10"
}
```
#### Response
HTTP/1.1 200 OK
**Content-Type:** `application/json`

```json
{
  "flights": [
    { "flightNumber": "UA123", "price": 350 }
  ]
}

![CMS API Screenshot](Screenshot%202025-08-06%20152720.png)
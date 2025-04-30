# HotelBooking - Hotel Reservation Management System

HotelBooking is a comprehensive solution for managing hotel room reservations. The system was developed to simplify the process of booking, managing, and tracking guests, rooms, and payments in hotels.

## Project Structure

The project is divided into three main parts:
1. **Client** - The client side, a Windows Forms application.
2. **Common** - A shared library containing common logic and data between the client and server.
3. **Server** - The server side, which manages the app and communicates with clients.

## Installation and Setup

Prerequisites

- .NET Core SDK (version 6.0 or newer)
- SQL Server (version 2019 or newer)
- Visual Studio 2022 (recommended) or any IDE supporting C# and .NET

## Installation Steps
1. **Clone the repository**:
git clone https://github.com/Noam-Gruber/HotelBooking.git
cd HotelBooking

2. **Database setup:**
	- Update the connection string in appsettings.json according to your database details
	- Run the Migration commands to create the database:

3. **Run the project**:
dotnet run
Or open the project in Visual Studio and run it from there.

4. **Access the system**:
Open a browser and navigate to https://localhost:5001 or the address specified during execution.

## Usage

1. Run the server.
2. Run the client.
3. Connect to the server and start playing.

## License

This project is licensed under the MIT License. For more details, see the license file.
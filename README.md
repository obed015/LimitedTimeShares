# Secure File Upload with Azure Blob Storage

A secure ASP.NET Core web application that enables users to upload files to Azure Blob Storage and generate time-limited, secure sharing links.

## Features

- Secure file upload to Azure Blob Storage
- Time-limited sharing links
- Automatic link expiration
- Secure access control
- Support for various file types
- Clean and intuitive user interface

## Technologies Used

- ASP.NET Core MVC
- Azure Blob Storage
- Azure Storage SDK
- Bootstrap for UI
- C# Programming Language

## Prerequisites

- Visual Studio 2022 or later
- .NET 8.0 SDK
- Azure Account with active subscription
- Azure Storage Account

## Setup and Configuration

1. Clone the repository
```bash
git clone https://github.com/yourusername/LimitedTimeShares.git

Create an Azure Storage Account if you haven't already

Go to Azure Portal
Create a new Storage Account
Get the connection string and account details


Configure the application

Create appsettings.Development.json file
Add your Azure Storage credentials:
{
  "BlobStorage": {
    "ConnectionString": "your_connection_string",
    "ContainerName": "your_container_name",
    "AccountName": "your_account_name",
    "AccountKey": "your_account_key"
  }
}

Run the application

Open the solution in Visual Studio
Restore NuGet packages
Build and run the application



Usage

Access the application through your web browser
Click on "Upload File" button
Select a file from your computer
Set the link expiration time (in hours)
Submit the upload
Copy the generated sharing link
Share the link with intended recipients

Security Features

Secure file storage in Azure Blob Storage
Time-limited access links
Azure Storage SAS tokens for secure access
Configuration security through Azure Key Vault support

Contributing
Feel free to fork the repository and submit pull requests for any improvements.

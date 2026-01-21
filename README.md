# ASP.NET 10 Multi-Language App

A .NET 10 MVC application demonstrating localization using `.resx` resource files.

## Features

- Multi-language support (English and Spanish)
- Resource-based localization using `.resx` files
- Language switcher in the UI

## Project Structure

```
src/
├── Controllers/        # MVC Controllers
├── Models/            # View Models
├── Resources/         # Localization resource files (.resx)
├── Views/             # Razor views
└── wwwroot/           # Static files (CSS, JS, images)
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Running the Application

```bash
cd src
dotnet restore
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Localization

Resource files are located in `src/Resources/`:

- `SharedResource.en.resx` - English translations
- `SharedResource.es.resx` - Spanish translations

To add a new language, create a new resource file following the pattern `SharedResource.{culture}.resx`.

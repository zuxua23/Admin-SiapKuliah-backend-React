# Admin SiapKuliah Backend

The backend API behind the SiapKuliah admin panel. It handles the game content (quizzes, puzzles, guess-the-picture), player data, feedback, and media uploads. The React admin frontend (Vite, `http://localhost:5173` in dev) talks to it.

## Stack

- .NET 8, ASP.NET Core Web API
- SQL Server, with all data access going through stored procedures
- `PolmanAstraLibrary` (an internal Polman Astra library) on top of `System.Data.SqlClient`
- Newtonsoft.Json for serialization, ClosedXML for Excel export
- Swagger via Swashbuckle (enabled in Development only)

## What it does

- Manages quizzes, puzzles and guess-the-picture items: create, edit, delete, view details, toggle active status
- Lists players and exports player data
- Collects and lists player feedback
- Handles file uploads into `wwwroot/Uploads` with unique names (`FILE_<guid>_<timestamp>`) and serves them back with HTTP Range support, so video seeking works
- Provides login, menu list and employee list endpoints
- HTML-encodes every request parameter (`Helper/EncodeData.cs`) before it reaches a stored procedure

## Project layout

```
Admin-SiapKuliah-backend/
├── Controllers/
│   ├── MasterQuizController.cs
│   ├── MasterPuzzleController.cs
│   ├── MasterTebakGambarController.cs
│   ├── MasterPemainController.cs
│   ├── MasterFeedbackController.cs
│   ├── UploadController.cs
│   └── UtilitiesController.cs
├── Helper/
│   ├── EncodeData.cs            # HTML-encodes request params
│   └── LDAPAuthentication.cs    # LDAP auth stub
├── Properties/launchSettings.json
├── wwwroot/Uploads/             # uploaded media
├── Program.cs
└── appsettings.json
```

## Before you start

You'll need:

- the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- a SQL Server instance with the `DB_SiapKuliah` database and its stored procedures (prefixed `skh_`, `sso_`, `all_`, `pro_`)
- `PolmanAstraLibrary.dll`, which is an internal library and is not in this repo

## Running it locally

Clone the repo:

```bash
git clone https://github.com/zuxua23/Admin-SiapKuliah-backend-React.git
cd Admin-SiapKuliah-backend-React
```

Point the DLL reference in `Admin-SiapKuliah-Backend.csproj` at wherever you keep the library:

```xml
<Reference Include="PolmanAstraLibrary">
  <HintPath>PATH\TO\PolmanAstraLibrary.dll</HintPath>
</Reference>
```

Set your connection string with user-secrets so credentials never end up in git:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<HOST>;Initial Catalog=DB_SiapKuliah;Integrated Security=false;TrustServerCertificate=true;user id=<USER>;password=<PASSWORD>;"
dotnet user-secrets set "Key:linkLDAP" "<LDAP_ADDRESS>"
```

Then run it:

```bash
dotnet restore
dotnet run --launch-profile http
```

The API listens on `http://localhost:5255` and Swagger is at `http://localhost:5255/swagger`.

## Configuration

| Key | Purpose |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `Key:linkLDAP` | LDAP address (auth is still a stub for now) |

CORS only allows `http://localhost:5173`. If you deploy this, change the `AllowSpecificOrigin` policy in `Program.cs`.

## API

Routes follow `api/{controller}/{action}`. Everything is a `POST` with a JSON body, except `GetFile`, which is a `GET`.

| Controller | Actions |
|---|---|
| `MasterQuiz` | `GetDataQuiz`, `GetDataQuizById`, `CreateQuiz`, `EditQuiz`, `DeleteQuiz`, `SetStatusQuiz`, `DetailQuiz` |
| `MasterPuzzle` | `GetDataPuzzle`, `GetDataPuzzleById`, `GetActivePuzzle`, `CreatePuzzle`, `EditPuzzle`, `DeletePuzzle`, `SetStatusPuzzle`, `DetailPuzzle` |
| `MasterTebakGambar` | `GetDataTebakGambar`, `GetDataTebakGambarById`, `GetActivetTebak`, `CreateTebakGambar`, `EditTebakGambar`, `DeleteTebakGambar`, `SetStatusTebakGambar`, `DetailTebakGambar` |
| `MasterPemain` | `GetDataPemain`, `CreatePemain`, `ExportPemain` |
| `MasterFeedback` | `GetDataFeedback`, `CreateFeedback` |
| `Upload` | `UploadFile` (multipart field `file`), `GetFile/{fileName}` |
| `Utilities` | `Login`, `GetListMenu`, `GetListKaryawan` |

Example call:

```bash
curl -X POST http://localhost:5255/api/MasterQuiz/GetDataQuiz \
  -H "Content-Type: application/json" \
  -d '{ ... }'
```

The body depends on the stored procedure behind the action. The order of the JSON properties is the order of the procedure's parameters, so keep it consistent.

## Good to know

- This repo is backend only. The React admin frontend lives in a separate repo.
- Business logic sits in the SQL Server stored procedures. The controllers are deliberately thin: parse the body, encode it, call the procedure, return the result.

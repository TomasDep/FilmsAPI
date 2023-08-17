# FilmsAPI

Movie application created in ASP.Net Core 6

## Technologies

-   ASP.Net CORE 6
-   Entity Framework Core
-   PostgreSQL
-   Swagger

## Commands

<div style="margin-bottom: 10px;">
    <strong>Create migration</strong>
</div>

```
dotnet ef migrations add <name-migration> --project route/to/FilmsAPI.Dao/FilmsAPI.Dao.csproj --startup-project route/to/FilmsAPI/FilmsAPI.csproj
```

<div style="margin-bottom: 10px;">
    <strong>Update database</strong>
</div>

```
dotnet ef database update --project route/to/FilmsAPI.Dao/FilmsAPI.Dao.csproj --startup-project route/to/FilmsAPI/FilmsAPI.csproj
```

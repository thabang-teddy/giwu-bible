
# Migration
## 1. **Ensure EF Core Tools Are Installed**
If you haven't installed the EF Core CLI tools, install them globally:

```sh
dotnet tool install --global dotnet-ef
```

Or update them if already installed:

```sh
dotnet tool update --global dotnet-ef
```

## 2. **Specify Connection String in `Website`**
In the `Website` project (`appsettings.json`), define your connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,8100;Database=fiobra;User Id=sa;Password=N5dK6sbYG287;TrustServerCertificate=True;"
  }
}
```

## 4. **Create the Migration**
Run the following command from the **root directory** where the `.sln` file is located:

```sh
dotnet ef migrations add InitialCreate --project DataAccess --startup-project Website --output-dir Migrations
```

- `--project DataAccess`: Saves the migration in `DataAccess`
- `--startup-project Website`: Uses `Website` as the entry point
- `--output-dir Migrations`: Saves migrations in `DataAccess/Migrations`

---

## 5. **Apply the Migration**
After generating the migration, apply it to the database:

```sh
dotnet ef database update --project DataAccess --startup-project Website
```

---

### 🎯 **Summary of Commands**
1. **Create migration**:
   ```sh
   dotnet ef migrations add InitialCreate --project DataAccess --startup-project Website --output-dir Migrations
   ```
2. **Apply migration**:
   ```sh
   dotnet ef database update --project DataAccess --startup-project Website
   ```

# docker
2. **docker**:

   ```sh
   docker build -t giwu_website_1 .

   ---

   docker run --name my-giwu -p 8080:8111 giwu_website_1
   ```
2. **docker compose**:
   ```sh
   docker compose -f docker-compose.dev.yml up --build -d
   ---
   docker compose -f docker-compose.Staging.yml up --build -d
   ---
   docker compose -f docker-compose.UAT.yml up --build -d
   ---
   docker compose -f docker-compose.Production.yml up --build  -d
   ```
 
# git

   ```sh
   git push live master
   ```

Now your database should be updated with the new schema! 🚀
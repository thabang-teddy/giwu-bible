
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

Now your database should be updated with the new schema! 🚀

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
dotnet ef migrations add InitialCreate --project DataAccess --startup-project Website --output-dir Migrations --context ApplicationDbContext

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
   dotnet ef migrations add InitialCreate --project DataAccess --startup-project Website --output-dir Migrations --context ApplicationDbContext
   
   ```
2. **Apply migration**:
   ```sh
   dotnet ef database update --project DataAccess --startup-project Website --context ApplicationDbContext
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
Using **Git hooks** to automatically deploy your website is a great idea — especially for VPS-based workflows.

Here’s how to set up **automatic deployment** using a `post-receive` hook on your VPS:

---

## 🚀 Git Hook Deployment Guide

### 🎯 Goal:
Push to a remote Git repo on your VPS → Automatically updates your Laravel / React / .NET website.

---

### 🗂️ VPS Setup: Create a Bare Git Repo

```bash
cd /var/www
mkdir giwubible.git
cd giwubible.git
git init --bare
```

---

### 🔧 Create `post-receive` Hook

```bash
nano /var/www/giwubible.git/hooks/post-receive
---

---
```bash
#!/bin/bash

GIT_WORK_TREE=/var/www/giwubible-production
GIT_DIR=/var/www/giwubible .git

echo "Deploying to $GIT_WORK_TREE..."

# Checkout the latest code
mkdir -p $GIT_WORK_TREE
git --work-tree=$GIT_WORK_TREE --git-dir=$GIT_DIR checkout -f

# Go into the deployment folder
cd $GIT_WORK_TREE

# Run Docker Compose build and up
docker-compose -f docker-compose.Production.yml up --build -d

Then make the script executable:
```bash
chmod +x /var/www/giwubible.git/hooks/post-receive
```

---

### 💻 Local Dev Machine: Add Remote

```bash
git remote add live ssh://user@your-vps-ip/var/www/giwubible.git
```

Then deploy with:
```bash
git push live master
```

---

### ✅ Output on push should look like:

```
Counting objects: ...
...
Deploying to /var/www/giwubible-live...
```

---

### 🔐 Permissions Tips:
- Make sure your VPS user has SSH access and write permission to `/var/www/giwubible-live`.
- You might want to `chown -R www-data:www-data` after deployment if using Nginx/Apache.

---

# git

   ```sh
   git remote set-url production ssh://deployer@your-vps-ip/var/www/giwubible.git
   ---
   git remote add production ssh://deployer@your-vps-ip/var/www/giwubible.git
   ---
   git remote remove production
   ---
   git push production master
   ```

Now your database should be updated with the new schema! 🚀
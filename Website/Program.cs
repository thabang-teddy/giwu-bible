using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models;
using Website.Services;

try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Configure SQL Server from connection string in config
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Configure Sqlite from connection string in config
    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options => {
        options.LoginPath = $"/Account/Auth/Login";
        options.LogoutPath = $"/Account/Auth/Logout";
        options.AccessDeniedPath = $"/Account/Auth/AccessDenied";
    });

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<AppDataService>();

    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();

    // Migrate DB on startup
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            Console.WriteLine("MyMQL databases have connected and responding.");
        }
        catch (Exception ex)
        {
            // Log the error or handle it (optional)
            Console.WriteLine("MyMQL databases connected failed: " + ex.Message);
            throw;
        }

        try
        {
            var sqlite = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var connected = await sqlite.BibleVersionKeys.AnyAsync();

            Console.WriteLine("SQLite databases have connected and responding.");
        }
        catch (Exception ex)
        {
            // Log the error or handle it (optional)
            Console.WriteLine("SQLite databases connected failed: " + ex.Message);
            throw;
        }
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Visitor}/{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    // Log the error or handle it (optional)
    Console.WriteLine("Database migration failed: " + ex.Message);
    //throw;

}
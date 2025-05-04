using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics; // Add this for debugging

namespace SQLiteToMySQLSync.Controllers
{
    public class SyncController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _uploadFolder;

        public SyncController(IConfiguration configuration)
        {
            _configuration = configuration;
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads"); // Store files in "uploads"
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        // GET: /Sync
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Sync/Upload
        [HttpPost("Sync/Upload")]
        public async Task<IActionResult> Upload(IFormFile sqliteFile, string host, string user, string password, string database)
        {
            if (sqliteFile == null || sqliteFile.Length == 0)
            {
                return Json(new { success = false, message = "Please select an SQLite file." });
            }

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(database))
            {
                return Json(new { success = false, message = "Please provide MySQL connection details." });
            }

            // Basic file type check (more robust check needed)
            if (!sqliteFile.FileName.EndsWith(".sqlite", StringComparison.OrdinalIgnoreCase) && !sqliteFile.FileName.EndsWith(".db", StringComparison.OrdinalIgnoreCase))
            {
                return Json(new { success = false, message = "Invalid file type. Only .sqlite and .db files are allowed." });
            }

            string safeFileName = Path.GetFileName(sqliteFile.FileName); // Get only the file name, without path
            string filePath = Path.Combine(_uploadFolder, safeFileName); // Combine with the upload folder

            try
            {
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await sqliteFile.CopyToAsync(stream);
                }

                // Perform the sync operation
                var syncResult = await SyncSQLiteToMySQL(filePath, host, user, password, database);

                // Delete the file after processing
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    // Log the error, but don't stop the process
                    Debug.WriteLine($"Error deleting file: {ex.Message}");
                }

                return Json(syncResult);
            }
            catch (Exception ex)
            {
                // Delete the file on error
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception)
                {
                    // Log the error, but don't stop the process
                    Debug.WriteLine("Error deleting file after error.");
                }
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        private async Task<object> SyncSQLiteToMySQL(string sqliteFilePath, string host, string user, string password, string database)
        {
            try
            {
                // 1. Connect to SQLite
                using (var sqliteConnection = new SQLiteConnection($"Data Source={sqliteFilePath}"))
                {
                    await sqliteConnection.OpenAsync();
                    using (var sqliteCommand = sqliteConnection.CreateCommand())
                    {
                        // Get the tables
                        sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_sequence';";
                        var sqliteTableNames = new List<string>();
                        using (var reader = await sqliteCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                sqliteTableNames.Add(reader.GetString(0));
                            }
                        }

                        // 2. Connect to MySQL
                        using (var mysqlConnection = new MySqlConnection($"Server={host};Database={database};Uid={user};Pwd={password};"))
                        {
                            await mysqlConnection.OpenAsync();
                            using (var mysqlCommand = mysqlConnection.CreateCommand())
                            {
                                // Loop through each table
                                foreach (var tableName in sqliteTableNames)
                                {
                                    // 3. Get the SQLite table schema
                                    sqliteCommand.CommandText = $"PRAGMA table_info({tableName})";
                                    var schema = new List<Dictionary<string, object>>();
                                    using (var reader = await sqliteCommand.ExecuteReaderAsync())
                                    {
                                        while (await reader.ReadAsync())
                                        {
                                            var column = new Dictionary<string, object>
                                            {
                                                { "name", reader["name"] },
                                                { "type", reader["type"] },
                                                { "notnull", Convert.ToBoolean(reader["notnull"]) },
                                                { "pk", Convert.ToBoolean(reader["pk"]) }
                                            };
                                            schema.Add(column);
                                        }
                                    }

                                    // 4. Create the table in MySQL
                                    string createTableSQL = $"CREATE TABLE IF NOT EXISTS {tableName} (";
                                    List<string> columns = new List<string>();
                                    foreach (var column in schema)
                                    {
                                        string columnDef = $"{column["name"]} {column["type"]}";
                                        if ((bool)column["notnull"]) columnDef += " NOT NULL";
                                        if ((bool)column["pk"]) columnDef += " PRIMARY KEY";
                                        columns.Add(columnDef);
                                    }
                                    createTableSQL += string.Join(", ", columns) + ")";
                                    mysqlCommand.CommandText = createTableSQL;
                                    await mysqlCommand.ExecuteNonQueryAsync();


                                    // 5. Get the data from SQLite
                                    sqliteCommand.CommandText = $"SELECT * FROM {tableName}";
                                    var data = new List<Dictionary<string, object>>();
                                    using (var reader = await sqliteCommand.ExecuteReaderAsync())
                                    {
                                        while (await reader.ReadAsync())
                                        {
                                            var row = new Dictionary<string, object>();
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                row[reader.GetName(i)] = reader.GetValue(i);
                                            }
                                            data.Add(row);
                                        }
                                    }

                                    // 6. Insert the data into MySQL
                                    if (data.Count > 0)
                                    {
                                        string insertSQL = $"INSERT INTO {tableName} ({string.Join(", ", data[0].Keys)}) VALUES ({string.Join(", ", System.Linq.Enumerable.Repeat("?", data[0].Count))})";
                                        mysqlCommand.CommandText = insertSQL;
                                        using (var cmd = mysqlConnection.CreateCommand())
                                        {
                                            cmd.CommandText = insertSQL;
                                            foreach (var row in data)
                                            {
                                                cmd.Parameters.Clear();
                                                foreach (var value in row.Values)
                                                {
                                                    cmd.Parameters.AddWithValue("@p", value); // Use a consistent parameter name
                                                }
                                                await cmd.ExecuteNonQueryAsync();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return new { success = true, message = "Data synced successfully." };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error syncing data: " + ex.Message };
            }
        }
    }
}


#!/bin/bash
echo "Waiting for SQL Server to be ready..."

until /opt/mssql-tools/bin/sqlcmd -S giwu_sqlserver -U SA -P 'N5dK6sbYG287' -Q "SELECT 1" > /dev/null 2>&1
do
  echo "SQL Server is not ready yet. Waiting..."
  sleep 2
done

echo "SQL Server is up. Starting the app..."
dotnet Website.dll

# Stage 1: build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# restore
COPY Models ./Models
COPY DataAccess ./DataAccess
COPY Website ./Website
RUN dotnet restore 'Website/Website.csproj'

# build
COPY Models ./Models
COPY DataAccess ./DataAccess
COPY Website ./Website

WORKDIR /src/Website
RUN dotnet build 'Website.csproj' -c Release -o /app/build

# stage 2: publish stage
FROM build AS publish
RUN dotnet publish 'Website.csproj' -c Release -o /app/publish

# Stage 3: run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=8111
ENV ASPNETCORE_ENVIRONMENT=Staging
# ENV ASPNETCORE_ENVIRONMENT=UAT
# ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8111
WORKDIR /app
COPY --from=publish /app/publish .

COPY datafiles /home/app/datafiles

ENTRYPOINT ["dotnet", "Website.dll"]

# COPY scripts/wait-for-sql.sh /app/wait-for-sql.sh
# RUN chmod +x /app/wait-for-sql.sh
# ENTRYPOINT ["/app/wait-for-sql.sh"]

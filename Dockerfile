# Use the official .NET 9 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /src

# Copy solution file and project files
COPY *.sln ./
COPY ITventory/*.csproj ./ITventory/
COPY ITventory.Application/*.csproj ./ITventory.Application/
COPY ITventory.Domain/*.csproj ./ITventory.Domain/
COPY ITventory.Infrastructure/*.csproj ./ITventory.Infrastructure/
COPY ITventory.Shared/*.csproj ./ITventory.Shared/
COPY ITventory.Shared.Abstractions/*.csproj ./ITventory.Shared.Abstractions/
COPY Tests/ITventory.Tests.Unit/*.csproj ./Tests/ITventory.Tests.Unit/

# Restore NuGet packages
RUN dotnet restore "ITventory/ITventory.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /src/ITventory
RUN dotnet build "ITventory.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ITventory.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Create a non-root user for security
RUN groupadd -r dotnet && useradd -r -g dotnet dotnet

# Set the working directory
WORKDIR /app

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Change ownership of the app directory to the dotnet user
RUN chown -R dotnet:dotnet /app

# Switch to the non-root user
USER dotnet

# Expose the port that the application will run on
EXPOSE 8080

# Set environment variables for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ITventory.dll"]

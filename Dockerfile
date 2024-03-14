FROM mcr.microsoft.com/dotnet/sdk:8.0 as BUILD
WORKDIR /app
EXPOSE 80

# Copy the solution file
COPY *.sln .
# Copy the project files for MarkingService
COPY MarkingService/*.csproj ./MarkingService/
RUN dotnet restore

# Copy the rest of the MarkingService source code
COPY MarkingService/. ./MarkingService/
WORKDIR /app/MarkingService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=BUILD /app/MarkingService/out ./

ENTRYPOINT ["dotnet", "MarkingService.dll"]

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and projects
COPY . .

# Restore and build
RUN dotnet restore InterviewZenika.sln
RUN dotnet build InterviewZenika.sln --configuration Debug --no-restore

# Run tests and export results
RUN dotnet test PlantMonitorUnitTest/PlantMonitorUnitTest.csproj \
    --logger "trx;LogFileName=results.trx" \
    --results-directory /src/TestResults \
    --no-build

# Final stage (optional, if you want a runtime image)
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /src/TestResults /app/TestResults

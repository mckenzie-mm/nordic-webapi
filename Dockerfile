# Stage 1: Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# restore
COPY ["src/webapi/webapi.csproj", "webapi/"]
RUN dotnet restore "webapi/webapi.csproj"

# build
COPY ["src/webapi", "webapi/"]
WORKDIR /src/webapi
RUN dotnet build "webapi.csproj" -c Release -o /app/build

# Stage 2: Publish stage
FROM build as publish
RUN dotnet publish "webapi.csproj" -c Release -o /app/publish

# Stage 3: Run stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "webapi.dll" ]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RedmineNotification.Api/RedmineNotification.Api.csproj", "RedmineNotification.Api/"]
RUN dotnet restore "RedmineNotification.Api/RedmineNotification.Api.csproj"
COPY . .
WORKDIR "/src/RedmineNotification.Api"
RUN dotnet build "RedmineNotification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedmineNotification.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedmineNotification.Api.dll"]

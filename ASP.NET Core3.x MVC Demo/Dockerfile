FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ASP.NET Core3.x MVC Demo/ASP.NET Core3.x MVC Demo.csproj", "ASP.NET Core3.x MVC Demo/"]
COPY ["Demo.Dto/Demo.Dto.csproj", "Demo.Dto/"]
COPY ["Common/Shared.Common.csproj", "Common/"]
RUN dotnet restore "ASP.NET Core3.x MVC Demo/ASP.NET Core3.x MVC Demo.csproj"
COPY . .
WORKDIR "/src/ASP.NET Core3.x MVC Demo"
RUN dotnet build "ASP.NET Core3.x MVC Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASP.NET Core3.x MVC Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:80
ENTRYPOINT ["dotnet", "ASP.NET Core3.x MVC Demo.dll"]
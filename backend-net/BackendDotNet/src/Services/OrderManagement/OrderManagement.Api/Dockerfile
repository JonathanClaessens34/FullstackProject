#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj", "Services/OrderManagement/OrderManagement.Api/"]
COPY ["BuildingBlocks/Domain/Domain.csproj", "BuildingBlocks/Domain/"]
COPY ["Services/OrderManagement/OrderManagement.AppLogic/OrderManagement.AppLogic.csproj", "Services/OrderManagement/OrderManagement.AppLogic/"]
COPY ["Services/OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj", "Services/OrderManagement/OrderManagement.Domain/"]
COPY ["Services/OrderManagement/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "Services/OrderManagement/OrderManagement.Infrastructure/"]
RUN dotnet restore "Services/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj"
COPY . .
WORKDIR "/src/Services/OrderManagement/OrderManagement.Api"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]
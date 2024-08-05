#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["G64.PedidoAPI/G64.PedidoAPI.csproj", "G64.PedidoAPI/"]
RUN dotnet restore "./G64.PedidoAPI/G64.PedidoAPI.csproj"
COPY . .
WORKDIR "/src/G64.PedidoAPI"
RUN dotnet build "./G64.PedidoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./G64.PedidoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copiar o banco de dados SQLite para o contêiner (opcional)
#COPY --from=build /src/data/app.db /app/data/
ENTRYPOINT ["dotnet", "G64.PedidoAPI.dll"]
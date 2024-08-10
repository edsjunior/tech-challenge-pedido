#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["G64.PedidoAPI/G64.PedidoAPI.csproj", "G64.PedidoAPI/"]
RUN dotnet restore "G64.PedidoAPI/G64.PedidoAPI.csproj"
COPY . .
WORKDIR "/src/G64.PedidoAPI"
RUN dotnet build "G64.PedidoAPI.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "G64.PedidoAPI.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "G64.PedidoAPI.dll"]

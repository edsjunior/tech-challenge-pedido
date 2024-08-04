#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# Install ca-certificates
RUN apt-get update && apt-get install -y ca-certificates

COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443
ENTRYPOINT ["dotnet", "G64.PedidoAPI.dll"]

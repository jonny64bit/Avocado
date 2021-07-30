#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Avocado.Web/Avocado.Web.csproj", "Avocado.Web/"]
RUN dotnet restore "Avocado.Web/Avocado.Web.csproj"
COPY . .
WORKDIR "/src/Avocado.Web"
RUN dotnet build "Avocado.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Avocado.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avocado.Web.dll"]
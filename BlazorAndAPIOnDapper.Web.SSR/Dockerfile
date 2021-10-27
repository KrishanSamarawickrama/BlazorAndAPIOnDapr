#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorAndAPIOnDapper.Web.SSR/BlazorAndAPIOnDapper.Web.SSR.csproj", "BlazorAndAPIOnDapper.Web.SSR/"]
RUN dotnet restore "BlazorAndAPIOnDapper.Web.SSR/BlazorAndAPIOnDapper.Web.SSR.csproj"
COPY . .
WORKDIR "/src/BlazorAndAPIOnDapper.Web.SSR"
RUN dotnet build "BlazorAndAPIOnDapper.Web.SSR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorAndAPIOnDapper.Web.SSR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorAndAPIOnDapper.Web.SSR.dll"]
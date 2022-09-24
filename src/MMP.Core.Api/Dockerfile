FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mmp-core/src/MMP.Core.Api/MMP.Core.Api.csproj", "MMP.Core.Api/"]
RUN dotnet restore "src/MMP.Core.Api/MMP.Core.Api.csproj"
COPY . .
WORKDIR "/src/MMP.Core.Api"
RUN dotnet build "MMP.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMP.Core.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMP.Core.Api.dll"]

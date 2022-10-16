FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MMP.Core.Bootstrap/MMP.Core.Bootstrap.csproj", "MMP.Core.Bootstrap/"]
RUN dotnet restore "src/MMP.Core.Bootstrap/MMP.Core.Bootstrap.csproj"
COPY . .
WORKDIR "/src/MMP.Core.Bootstrap"
RUN dotnet build "MMP.Core.Bootstrap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMP.Core.Bootstrap.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMP.Core.Bootstrap.dll"]

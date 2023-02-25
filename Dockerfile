FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/src/MMP.Core.Api/MMP.Core.Api.csproj", "src/MMP.Core.Api/"]
COPY ["/src/MMP.Core.Application/MMP.Core.Application.csproj", "src/MMP.Core.Application/"]
COPY ["/src/MMP.Core.Domain/MMP.Core.Domain.csproj", "src/MMP.Core.Domain/"]
COPY ["/src/MMP.Core.Shared/MMP.Core.Shared.csproj", "src/MMP.Core.Shared/"]

RUN dotnet restore "src/MMP.Core.Api/MMP.Core.Api.csproj"
COPY . .
WORKDIR "src/MMP.Core.Api"
RUN ls
RUN dotnet build "MMP.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMP.Core.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMP.Core.Api.dll"]

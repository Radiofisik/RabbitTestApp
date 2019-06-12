FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["RabbitTestApp/RabbitTestApp.csproj", "RabbitTestApp/"]
RUN dotnet restore "RabbitTestApp/RabbitTestApp.csproj"
COPY . .
WORKDIR "/src/RabbitTestApp"
RUN dotnet build "RabbitTestApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RabbitTestApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RabbitTestApp.dll"]
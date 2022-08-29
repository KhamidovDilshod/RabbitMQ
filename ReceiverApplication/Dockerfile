FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitMQ/ReceiverApplication/ReceiverApplication.csproj", "ReceiverApplication/"]
RUN dotnet restore "ReceiverApplication/ReceiverApplication.csproj"
COPY . .
WORKDIR "/src/ReceiverApplication"
RUN dotnet build "ReceiverApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReceiverApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReceiverApplication.dll"]

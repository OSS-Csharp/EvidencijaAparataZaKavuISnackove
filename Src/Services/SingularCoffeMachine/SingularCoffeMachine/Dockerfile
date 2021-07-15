FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["SingularCoffeMachine/SingularCoffeMachine.csproj", "SingularCoffeMachine/"]
RUN dotnet restore "SingularCoffeMachine\SingularCoffeMachine.csproj"
COPY . .
WORKDIR "/src/SingularCoffeMachine"
RUN dotnet build "SingularCoffeMachine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SingularCoffeMachine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SingularCoffeMachine.dll"]

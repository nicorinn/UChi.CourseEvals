FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UChi.CourseEvals.Api/UChi.CourseEvals.Api.csproj", "UChi.CourseEvals.Api/"]
RUN dotnet restore "UChi.CourseEvals.Api/UChi.CourseEvals.Api.csproj"
COPY . .
WORKDIR "/src/UChi.CourseEvals.Api"
RUN dotnet build "UChi.CourseEvals.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UChi.CourseEvals.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UChi.CourseEvals.Api.dll"]

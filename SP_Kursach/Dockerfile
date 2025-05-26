FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
RUN apt-get update && apt-get install -y libgdiplus
COPY --from=build /app/out ./
ENV OUTPUT_DIR=/app/output
ENTRYPOINT ["dotnet", "SP_Kursach.dll"]
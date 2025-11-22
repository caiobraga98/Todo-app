# ===============================================
# STAGE 1: BUILD (Compilação do Código)
# ===============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# 🎯 Copia o arquivo do projeto da raiz do contexto (o ponto ".")
COPY ToDo-app.csproj . 
RUN dotnet restore

# Copia o restante do código-fonte e o publica
COPY . .
RUN dotnet publish "ToDo-app.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore

# ===============================================
# STAGE 2: FINAL (Execução)
# ===============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ToDo-app.dll"]
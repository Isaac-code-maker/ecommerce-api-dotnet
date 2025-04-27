# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto
COPY . ./

# Restaurar as dependências
RUN dotnet restore

# Compilar o projeto
RUN dotnet build -c Release -o /app/build

# Publicar o projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados
COPY --from=build /app/publish .

# Expor a porta padrão do ASP.NET Core
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "RO.DevTest.WebApi.dll"]